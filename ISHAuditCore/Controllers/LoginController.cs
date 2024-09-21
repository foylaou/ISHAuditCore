using ISHAuditCore.Context;
using ISHAuditCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore; // for password hashing


namespace ISHAuditCore.Controllers
{
    public class loginController(ISHAuditDbcontext dbContext) : Controller
    {
        
        // 驗證 Cloudflare CAPTCHA
        private async Task<bool> VerifyCaptcha(string cfTurnstileResponse)
        {
            var secretKey = "0x4AAAAAAAgCSfX3281Nkep8uqXE-1Hok8k"; // Cloudflare Secret Key

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", secretKey),
                    new KeyValuePair<string, string>("response", cfTurnstileResponse),
                    new KeyValuePair<string, string>("remoteip", Request.HttpContext.Connection.RemoteIpAddress.ToString())
                }));

                var responseString = await response.Content.ReadAsStringAsync();
                var captchaResult = JsonConvert.DeserializeObject<Cloudfare.captchaVerificationResponse>(responseString);

                return captchaResult.Success;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        // 用於處理登入資料的 POST 請求
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Index(string username, string password ,string cfTurnstileResponse)
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
             Thread.Sleep(5000);
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
            var user = dbContext.user_infos.FirstOrDefault(u => u.id == userId);
            if (user != null)
            {
                Codes codeClass = new Codes();

                HttpContext.Session.SetString("user_id", user.id.ToString());
                if (user.username != null) HttpContext.Session.SetString("username", user.username);
                if (user.nickname != null) HttpContext.Session.SetString("nickname", user.nickname);
                HttpContext.Session.SetString("enterprise_id", user.enterprise_id.ToString() ?? string.Empty);
                HttpContext.Session.SetString("company_id", user.company_id.ToString() ?? throw new InvalidOperationException());
                HttpContext.Session.SetString("factory_id", user.factory_id.ToString() ?? throw new InvalidOperationException());
                HttpContext.Session.SetString("factory_name", codeClass.FactoryName(user.factory_id.ToString() ?? throw new InvalidOperationException()));
                HttpContext.Session.SetString("company_name", codeClass.CompanyName(user.company_id.ToString() ?? throw new InvalidOperationException()));
                HttpContext.Session.SetString("anyllmworkspace", user.anyllmworkspace ?? throw new InvalidOperationException());

                if (HttpContext.Session.GetString("authority") != null)
                {
                    var authority = JsonConvert.DeserializeObject<Authority>(HttpContext.Session.GetString("authority") ?? throw new InvalidOperationException());
                    HttpContext.Session.SetString("authority", user.authority ?? throw new InvalidOperationException());
                    if (authority is { Sys: "admin" })
                    {
                        authority.Sys = "admin";
                        HttpContext.Session.SetString("authority", JsonConvert.SerializeObject(authority));
                    }
                }
            }
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // 清除所有 session
            return RedirectToAction("Index");
        }
    }
}