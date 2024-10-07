using ISHAudit.Models;
using ISHAuditCore.Context;
using ISHAuditCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISHAuditCore.Controllers
{
    public class SignUpController : baseController
    {
        private readonly signUpService _signUpService;

        // 控制器建構函數，注入 SignUpService 和其他依賴
        public SignUpController(signUpService signUpService, ISHAuditDbcontext dbContext, Authority authorityClass)
            : base(dbContext, authorityClass) // 呼叫 baseController 的構造函數
        {
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
    }
}