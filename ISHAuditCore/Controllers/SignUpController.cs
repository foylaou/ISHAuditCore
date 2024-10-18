using System.Text.RegularExpressions;
using ISHAudit.Models;
using ISHAuditCore.Context;
using ISHAuditCore.Migrations.Model;
using ISHAuditCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISHAuditCore.Controllers
{
    public class SignUpController : baseController
    {
        private readonly signUpService _signUpService;
        private readonly ISHAuditDbcontext _db;
        
        // 控制器建構函數，注入 SignUpService 和其他依賴
        public SignUpController(signUpService signUpService, ISHAuditDbcontext dbContext, Authority authorityClass, UserEditService userEditService)
            : base(dbContext, authorityClass,userEditService) // 呼叫 baseController 的構造函數
        {
            _db = dbContext;
            _signUpService = signUpService ?? throw new ArgumentNullException(nameof(signUpService));
        }

        // 頁面方法
        public IActionResult Index()
        {
            var enterprises = _signUpService.GetEnterpriseList();
            var companies = _signUpService.GetCompanyList();
            var factories = _signUpService.GetFactoryList();

            ViewBag.EnterpriseJson = JsonConvert.SerializeObject(enterprises);
            ViewBag.CompanyJson = JsonConvert.SerializeObject(companies);
            ViewBag.FactoryJson = JsonConvert.SerializeObject(factories);

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection post)
        {
            if (!Regex.IsMatch(post["password"], @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$"))
            {
                var errorResponse = new { error = "密碼不符合規則:" + post["password"] + "\n 請最少輸入8-20位大小寫混和英文與數字" };
                return Json(errorResponse);
            }
            else
            {
                byte[] salt = new byte[128 / 8];
                string username = post["username"];
                string password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: post["password"],
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                string nickname = post["nickname"];
                Nullable<Int16> enterpriseId = null;
                Nullable<Int16> companyId = null;
                Nullable<Int16> factoryId = null;
                if (post["enterprise_id"] != "" || post["company_id"] != "" || post["factory_id"] != "")
                {
                    if (post["enterprise_id"] != "")
                    {
                        enterpriseId = Convert.ToInt16(post["enterprise_id"]);
                    }

                    if (post["company_id"] != "")
                    {
                        companyId = Convert.ToInt16(post["company_id"]);
                    }

                    if (post["factory_id"] != "")
                    {
                        factoryId = Convert.ToInt16(post["factory_id"]);
                    }
                }


                // Access specific fields
                string audit = post["Audit"];
                string kpi = post["KPI"];
                string sys = post["Sys"];
                string org = post["Org"];

                Dictionary<string, string> values = new Dictionary<string, string>
                {
                    { "Audit", audit },
                    { "KPI", kpi },
                    { "Sys", sys },
                    { "Org", org }
                };
                string authority = JsonConvert.SerializeObject(values);

                
                if (_db.user_infos.Any(u => u.username == username))
                {
                    var errorResponse = new { error = "此帳號已有人使用" };
                    return Json(errorResponse);
                }

                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                DateTime? parsedDate = DateTime.TryParse(formattedDate, out DateTime result)
                    ? (DateTime?)result
                    : null;
                user_info newUser = new user_info
                {
                    username = username,
                    password = password,
                    nickname = nickname,
                    enterprise_id = enterpriseId,
                    company_id = companyId,
                    factory_id = factoryId,
                    authority = authority,
                    created_at = parsedDate,
                };

                _db.user_infos.Add(newUser);
                _db.SaveChanges();

                var successResponse = new { message = "註冊成功" };
                return Ok(successResponse);
                
            }
        }
    }
}