using ISHAudit.Models;
using ISHAuditCore.Context;
using ISHAuditCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Serilog; // for password hashing


namespace ISHAuditCore.Controllers
{
    public class loginController(ISHAuditDbcontext dbContext, IConfiguration configuration, UserEditService _userEditService) : Controller
    {
        /// <summary>
        /// 驗證 Cloudflare CAPTCHA 回應
        /// </summary>
        /// <param name="cfTurnstileResponse">來自前端的 Cloudflare CAPTCHA 驗證回應</param>
        /// <returns>回傳是否驗證成功的布林值，若成功則回傳 true，否則 false</returns>
        public async Task<bool> VerifyCaptcha(string cfTurnstileResponse)
        {
            // 從 appsettings.json 中取得 Cloudflare 圖靈驗證密鑰
            var secretKey = configuration["Cloudflare:CaptchaSecretKey"]; // Cloudflare Secret Key
            using (var client = new HttpClient())
            {
                // 向 Cloudflare 驗證伺服器發送驗證請求
                var response = await client.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify",
                    new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("secret", secretKey), // 發送密鑰
                        new KeyValuePair<string, string>("response", cfTurnstileResponse), // 發送 CAPTCHA 回應
                        new KeyValuePair<string, string>("remoteip",
                            Request.HttpContext.Connection.RemoteIpAddress.ToString()) // 發送用戶的 IP 地址
                    }));
        
                // 將 Cloudflare 回應轉換成字串格式
                var responseString = await response.Content.ReadAsStringAsync();

                // 將 JSON 回應反序列化為 captchaVerificationResponse 類型
                var captchaResult =
                    JsonConvert.DeserializeObject<Cloudfare.captchaVerificationResponse>(responseString);

                // 回傳驗證是否成功的布林值
                return captchaResult.Success;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test2()
        {
            return View();
        }
        public IActionResult Test()
        {

            try
            {
                // 故意拋出一個除以零的錯誤
                int zero = 0;
                int result = 10 / zero;
            }
            catch (Exception ex)
            {
                // 使用 Serilog 記錄錯誤訊息
                Log.Error(ex, "A division by zero error occurred.");
                
            }

            // 返回簡單的文字內容而不是視圖
            return Content($"Test action completed. Check the logs for the error.");
        }

        // 用於處理登入資料的 POST 請求
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string password, string cfTurnstileResponse)
        {
            if (string.IsNullOrEmpty(cfTurnstileResponse))
            {
                // CAPTCHA 回應為空，拒絕請求
                return Json(new { error = "CAPTCHA 驗證失敗" });
            }

            // 檢查 CAPTCHA 驗證
            if (!await VerifyCaptcha(cfTurnstileResponse))
            {
                return Json(new { error = "CAPTCHA 驗證失敗" });
            }

            if (string.IsNullOrEmpty(password))
            {
                return Json(new { error = "密碼不能為空" });
            }

            var user = await dbContext.user_infos.FirstOrDefaultAsync(u => u.username == username);
            //使用異步查詢 優化反應速度 原（937ms => 1ms)
            if (user == null)
            {
                return Json(new { error = "使用者不存在" });
            }

            byte[] salt = new byte[128 / 8];
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (user.password == hashedPassword)
            {
                HttpContext.Session.SetString("login", "login");
                WrSession(user.id);
                if (user.authority != null)
                {
                    HttpContext.Session.SetString("authority", user.authority);
                }

                return Json(new { message = "登錄成功", redirectUrl = Url.Action("Index", "Home") });
            }
            //異步延遲
            await Task.Delay(5000);
            return Json(new { error = "密碼錯誤" });
        }

        [HttpPost]
        public JsonResult ChangeAccount(int userAccount)
        {
            WrSession(userAccount);
            return Json(new { result = "True" });
        }

        private void WrSession(int userId)
        {
            // Fetch the auth list for the specified userId
            var authList = _userEditService.GetAuth(userId);

            // Check if we have any auth data
            if (authList != null && authList.Count > 0)
            {
                var user = authList.First(); // Get the first auth object

                // Set session values from the retrieved auth object
                HttpContext.Session.SetString("user_id", user.user_id);
                HttpContext.Session.SetString("username", user.username);
                HttpContext.Session.SetString("nickname", user.nickname);
                HttpContext.Session.SetString("enterprise_id", user.enterprise_id);
                HttpContext.Session.SetString("company_id", user.company_id);
                HttpContext.Session.SetString("factory_id", user.factory_id);
                HttpContext.Session.SetString("factory_name", user.factory_name);
                HttpContext.Session.SetString("company_name", user.company_name);

                // Check if the authority is already in the session
                if (HttpContext.Session.GetString("authority") != null)
                {
                    var authority = JsonConvert.DeserializeObject<Authority>(HttpContext.Session.GetString("authority"));
                    var SysAuthority = authority.Sys;

                    // If the user's system authority is admin, update it
                    if (SysAuthority == "admin")
                    {
                        authority.Sys = "admin";
                        HttpContext.Session.SetString("authority", JsonConvert.SerializeObject(authority));
                    }
                }
            }
            else
            {
                // Handle the case where no auth data is found (optional)
                // e.g., log an error, clear session, etc.
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // 清除所有 session
            return RedirectToAction("Index");
        }
    }
}