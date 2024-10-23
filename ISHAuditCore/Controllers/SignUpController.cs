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
        private readonly UserEditService _userEditService;
        private readonly ISHAuditDbcontext _db;
        
        // 控制器建構函數，注入 SignUpService 和其他依賴
        public SignUpController(ISHAuditDbcontext dbContext, Authority authorityClass, UserEditService userEditService)
            : base(dbContext, authorityClass,userEditService) // 呼叫 baseController 的構造函數
        {
            _db = dbContext;
            _userEditService = userEditService ?? throw new ArgumentNullException(nameof(userEditService));
        }

        // 頁面方法
        public IActionResult Index()
        {
            var enterprises = _userEditService.GetEnterpriseList();
            ViewBag.EnterpriseJson = JsonConvert.SerializeObject(enterprises);
            return View();
        }
        
        
        /// <summary>
        /// 用於處理使用者註冊的動作，並進行密碼驗證、企業相關資訊處理和儲存註冊資料到資料庫。
        /// </summary>
        /// <param name="post">包含使用者輸入的註冊資訊的表單資料。</param>
        /// <returns>回傳一個包含註冊結果的 JSON 響應，若成功註冊則回傳成功訊息，若有錯誤則回傳錯誤訊息。</returns>
        /// <remarks>
        /// 此方法的主要功能為：
        /// 
        /// 1. 驗證密碼格式是否符合要求（至少 8-20 位，包含大小寫字母和數字）。
        /// 2. 根據使用者輸入處理企業（enterprise）、公司（company）、工廠（factory）的 ID 資訊，若沒有輸入則設為 null。
        /// 3. 生成使用者的權限資訊（Audit、KPI、Sys、Org）並序列化為 JSON 字符串保存。
        /// 4. 檢查是否已存在相同帳號名稱的使用者，若存在則回傳錯誤訊息。
        /// 5. 將新註冊的使用者資訊（包括帳號、加密後的密碼、暱稱、權限等）儲存到資料庫中。
        /// 6. 註冊成功後回傳成功訊息。
        /// 
        /// <example>
        /// 密碼格式驗證的正則表達式為：^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$，至少要包含一個小寫字母、一個大寫字母和一個數字，長度為 8 至 20 位。
        /// </example>
        /// </remarks>
        /// <exception cref="ArgumentException">如果使用者帳號已經存在，將回傳錯誤訊息。</exception>
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