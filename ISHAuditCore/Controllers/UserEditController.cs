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
    public class UserEditController: baseController
    {
        private readonly UserEditService _userEditService;
        private readonly ISHAuditDbcontext _db;
        private readonly UserData _userData;
        public UserEditController(UserEditService userEditService, ISHAuditDbcontext dbContext, Authority authorityClass,UserData userData)
            : base(dbContext, authorityClass) // 呼叫 baseController 的構造函數
        {
            _db = dbContext;
            _userData = userData;
            _userEditService = userEditService ?? throw new ArgumentNullException(nameof(userEditService));
        }
        
        public IActionResult Index()
        {
            var enterprises = _userEditService.GetEnterpriseList();
            var companies = _userEditService.GetCompanyList();
            var factories = _userEditService.GetFactoryList();

            ViewBag.EnterpriseJson = JsonConvert.SerializeObject(enterprises);
            ViewBag.CompanyJson = JsonConvert.SerializeObject(companies);
            ViewBag.FactoryJson = JsonConvert.SerializeObject(factories);

            return View();
        }
        
        [HttpGet]
        public JsonResult GetDataFromDatabase()
        {
            List<UserDataSet> dataFromDatabase = _userData.DataFromDatabase();
            return Json(dataFromDatabase);
        }

        [HttpPost]
        public ActionResult SaveData(user_info viewModel)
        {

            if (viewModel == null)
            {
                return Json(new { error = "無效請求" });
            }


            if (!string.IsNullOrWhiteSpace(viewModel.password) && Regex.IsMatch(viewModel.password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^]).{8,20}$"))
            {
                return Json(new { error = "密碼不符合规则，請最少输入8-20位大小寫混合英文與数字" });
            }


            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            DateTime? parsedDate = DateTime.TryParse(formattedDate, out DateTime result)
                ? (DateTime?)result
                : null;
            var data = _db.user_infos.FirstOrDefault(item => item.id == viewModel.id);
            if (data.password != viewModel.password)
            {
                byte[] salt = new byte[128 / 8];
                string n_password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: viewModel.password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                if (data != null)
                {
                    // 更新数据
                    data.nickname = viewModel.nickname;
                    data.update_at = parsedDate;

                    // 保存到数据库
                    _db.SaveChanges();

                    return Json(new { message = "更新成功" });
                }
            }
            if (data != null)
            {
                // 更新数据
                data.nickname = viewModel.nickname;
                data.update_at = parsedDate;

                // 保存到数据库
                _db.SaveChanges();
                
                List<UserDataSet> dataFromDatabase = _userData.DataFromDatabase();
                return Json(dataFromDatabase);
            }
            else
            {
                return Json(new { error = "未找到用户數據" });
            }
            
        }

        [HttpPost]
        public ActionResult Newpassword(IFormCollection post)
        {

            if (!Regex.IsMatch(post["password"], @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$"))
            {
                var errorResponse = new { error = "密碼不符合規則:" + post["password"] + "\n 請最少輸入8-20位大小寫混和英文與數字" };
                return Json(errorResponse);
            }
            byte[] salt = new byte[128 / 8];
            Int16 id = Convert.ToInt16(post["id"]);
            string password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: post["password"],
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));


            
            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            DateTime? parsedDate = DateTime.TryParse(formattedDate, out DateTime result)
                ? (DateTime?)result
                : null;
            var data = _db.user_infos.FirstOrDefault(item => item.id == id);
            if (post != null)
            {
                data.password = password;
                data.update_at = parsedDate;
                _db.SaveChanges();


            }
            List<UserDataSet> dataFromDatabase = _userData.DataFromDatabase();
            return Json(dataFromDatabase);
        }

        [HttpPost]
        public ActionResult Newauthority(IFormCollection post)
        {
            // Access specific fields
            string Audit = post["Audit"];
            string KPI = post["KPI"];
            string Sys = post["Sys"];
            string Org = post["Org"];
            Int16 id = Convert.ToInt16(post["id"]);
            var authority = new Authority
            {
                Audit = Audit,
                KPI = KPI,
                Sys = Sys,
                Org = Org
            };


            var data = _db.user_infos.FirstOrDefault(item => item.id == id);
            if (data != null)
            {
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                DateTime? parsedDate = DateTime.TryParse(formattedDate, out DateTime result)
                    ? (DateTime?)result
                    : null;
                string authorityJson = JsonConvert.SerializeObject(authority);
                data.authority = authorityJson;
                data.update_at = parsedDate;
                _db.SaveChanges();

            }
            List<UserDataSet> dataFromDatabase = _userData.DataFromDatabase();
            return Json(dataFromDatabase);
        }
        [HttpPost]
        public ActionResult Newenterprise(IFormCollection post)
        {
            // Access specific fields
            Nullable<Int16> enterprise_id = null;
            Nullable<Int16> company_id = null;
            Nullable<Int16> factory_id = null;
            if (post["enterprise_id"] != "" || post["company_id"] != "" || post["factory_id"] != "")
            {
                if (post["enterprise_id"] != "")
                {
                    enterprise_id = Convert.ToInt16(post["enterprise_id"]);
                }

                if (post["company_id"] != "")
                {
                    company_id = Convert.ToInt16(post["company_id"]);
                }

                if (post["factory_id"] != "")
                {
                    factory_id = Convert.ToInt16(post["factory_id"]);
                }
            }
            Int16 id = Convert.ToInt16(post["id"]);

            
            var data = _db.user_infos.FirstOrDefault(item => item.id == id);
            if (data != null)
            {
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                DateTime? parsedDate = DateTime.TryParse(formattedDate, out DateTime result)
                    ? (DateTime?)result
                    : null;
                data.enterprise_id = enterprise_id;
                data.company_id = company_id;
                data.factory_id = factory_id;
                data.update_at = parsedDate;
                _db.SaveChanges();

            }
            List<UserDataSet> dataFromDatabase = _userData.DataFromDatabase();
            return Json(dataFromDatabase);
        }
        [HttpPost]
        public IActionResult DeleteData([FromBody] List<int> ids)
        {
            try
            {
                var usersToDelete = _db.user_infos.Where(u => ids.Contains(u.id)).ToList();
                if (usersToDelete.Any())
                {
                    _db.user_infos.RemoveRange(usersToDelete);  // 刪除對應的資料
                    _db.SaveChanges();  // 保存更改
                }
                return Ok(new { message = "刪除成功" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "刪除失敗", error = ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetUserDataById(int userId)
        {
            // 調用 DataFromDatabase 並根據 userId 過濾
            var userData = _userData.DataFromDatabase()
                .Where(user => user.Id == userId)
                .Select(user => new
                {
                    EnterpriseName = user.EnterpriseName != null ? user.EnterpriseName : null, // 防止 null
                    EnterpriseId = user.EnterpriseId != null ? user.EnterpriseId : (int?)null, 
                    CompanyName = user.CompanyName != null ? user.CompanyName : null,
                    CompanyId = user.CompanyId != null ? user.CompanyId : (int?)null, // 防止 null
                    FactoryName = user.FactoryName != null ? user.FactoryName : null, // 防止 null
                    FactoryId = user.FactoryId != null ? user.FactoryId : (int?)null
                    
                }).FirstOrDefault();

            if (userData != null)
            {
                return Json(userData);
            }

            return Json(null);
        }
    }
}
