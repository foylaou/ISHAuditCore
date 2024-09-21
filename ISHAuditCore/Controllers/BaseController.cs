using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ISHAuditCore.Context;
using ISHAuditCore.Models;

namespace ISHAuditCore.Controllers
{
    public class baseController(ISHAuditDbcontext dbContext, Authority authorityClass) : Controller
    {
        // 通過依賴注入注入 DbContext 和 Authority


        // Action Filter: OnActionExecuting
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sController = filterContext.RouteData.Values["controller"].ToString()!.ToUpper();
            var sBypassController = "Auditedit";

            if (sController != sBypassController.ToUpper())
            {
                // AutoLogin();

                if (HttpContext.Session.GetString("login") == null ||
                    string.IsNullOrWhiteSpace(HttpContext.Session.GetString("login")))
                {
                    filterContext.Result = Redirect("~/Login");
                }
                else
                {
                    var sAction = filterContext.RouteData.Values["action"].ToString().ToUpper();
                    var authority =
                        JsonConvert.DeserializeObject<Authority>(HttpContext.Session.GetString("authority"));
                    var SysAuthority = authority.Sys.ToUpper();
                    var AuditAuthority = authority.Audit.ToUpper();
                    var KpiAuthority = authority.KPI.ToUpper();

                    if (!authorityClass.ModalAuthority(sController, SysAuthority, AuditAuthority, KpiAuthority) ||
                        !authorityClass.FunctionAuthority(sController, sAction, SysAuthority, AuditAuthority,
                            KpiAuthority, HttpContext.Session.GetString("factory_id")))
                    {
                        filterContext.Result = Redirect("~/");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private void AutoLogin()
        {
            var user = dbContext.user_infos.FirstOrDefault(u => u.id == 1);
            if (user != null)
            {
                HttpContext.Session.SetString("login", "login");
                WrSession(user.id);
                HttpContext.Session.SetString("authority", user.authority);
            }
        }

        public void WrSession(int userId)
        {
            var user = dbContext.user_infos.FirstOrDefault(u => u.id == userId);
            if (user != null)
            {
                var codeClass = new Codes();

                HttpContext.Session.SetString("user_id", user.id.ToString());
                HttpContext.Session.SetString("username", user.username);
                HttpContext.Session.SetString("nickname", user.nickname);
                HttpContext.Session.SetString("enterprise_id", user.enterprise_id.ToString());
                HttpContext.Session.SetString("company_id", user.company_id.ToString());
                HttpContext.Session.SetString("factory_id", user.factory_id.ToString());
                HttpContext.Session.SetString("factory_name", codeClass.FactoryName(user.factory_id.ToString()));
                HttpContext.Session.SetString("company_name", codeClass.CompanyName(user.company_id.ToString()));

                if (HttpContext.Session.GetString("authority") != null)
                {
                    var authority = JsonConvert.DeserializeObject<Authority>(HttpContext.Session.GetString("authority"));
                    var SysAuthority = authority.Sys;

                    if (SysAuthority == "admin")
                    {
                        authority.Sys = "admin";
                        HttpContext.Session.SetString("authority", JsonConvert.SerializeObject(authority));
                    }
                }
            }
        }
    }
}