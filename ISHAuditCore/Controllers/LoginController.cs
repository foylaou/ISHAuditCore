using ISHAuditCore.Context;
using ISHAuditCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cryptography.KeyDerivation; // for password hashing
using Microsoft.AspNetCore.Http; // for session access

namespace ISHAuditCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISHAuditDbcontext _db; // 注入的資料庫上下文

        public LoginController(ISHAuditDbcontext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string username, string password)
        {
            byte[] salt = new byte[128 / 8];
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // 驗證帳號密碼
            var user = _db.user_infos.FirstOrDefault(u => u.username == username);
            if (user != null && user.password == hashedPassword)
            {
                HttpContext.Session.SetString("login", "login");
                WrSession(user.id);
                if (user.authority != null) HttpContext.Session.SetString("authority", user.authority);
                return RedirectToAction("Index", "Home");
            }

            // 如果驗證失敗，返回 Index 視圖
            return View();
        }

        [HttpPost]
        public JsonResult ChangeAccount(int user_account)
        {
            WrSession(user_account);
            return Json(new { result = "True" });
        }

        private void WrSession(int userId)
        {
            var user = _db.user_infos.FirstOrDefault(u => u.id == userId);
            if (user != null)
            {
                Codes CodeClass = new Codes();

                HttpContext.Session.SetString("user_id", user.id.ToString());
                HttpContext.Session.SetString("username", user.username);
                HttpContext.Session.SetString("nickname", user.nickname);
                HttpContext.Session.SetString("enterprise_id", user.enterprise_id.ToString());
                HttpContext.Session.SetString("company_id", user.company_id.ToString());
                HttpContext.Session.SetString("factory_id", user.factory_id.ToString());
                HttpContext.Session.SetString("factory_name", CodeClass.FactoryName(user.factory_id.ToString()));
                HttpContext.Session.SetString("company_name", CodeClass.CompanyName(user.company_id.ToString()));
                HttpContext.Session.SetString("anyllmworkspace", user.anyllmworkspace);

                if (HttpContext.Session.GetString("authority") != null)
                {
                    var authority = JsonConvert.DeserializeObject<Authority>(HttpContext.Session.GetString("authority"));
                    HttpContext.Session.SetString("authority", user.authority);
                    if (authority.Sys == "admin")
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