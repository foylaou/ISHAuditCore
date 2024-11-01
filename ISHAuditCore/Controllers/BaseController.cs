using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ISHAuditCore.Context;
using ISHAuditCore.Models;

namespace ISHAuditCore.Controllers
{
    public class baseController(ISHAuditDbcontext dbContext, Authority authorityClass, UserEditService _userEditService) : Controller
    {
        // 通過依賴注入注入 DbContext 和 Authority


        /// <summary>
        /// 這個方法在執行控制器操作之前執行，主要是檢查使用者的登入狀態和權限。
        /// </summary>
        /// <param name="filterContext">Action 執行的上下文，包含路由資料和 HTTP 請求的相關資訊。</param>
        /// <remarks>
        /// 1. 檢查當前控制器名稱，若不是 'Auditedit' 則進行進一步的權限驗證。
        /// 2. 檢查使用者的登入狀態，若未登入則重導向到登入頁面。
        /// 3. 驗證使用者的權限，若使用者沒有足夠的權限訪問某些控制器或操作，將重導向首頁。
        /// 權限檢查會根據使用者 Session 中儲存的權限資料進行判斷。
        /// </remarks>
        /// <exception cref="UnauthorizedAccessException">如果使用者未登入或沒有足夠的權限時，會自動進行頁面重導向。</exception>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sController = filterContext.RouteData.Values["controller"].ToString()!.ToUpper();
            var sBypassController = "Auditedit";

            if (sController != sBypassController.ToUpper())
            {
                //AutoLogin();

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
                HttpContext.Session.SetString("enterprise_id", user.enterprise_id?.ToString() ?? string.Empty);
                HttpContext.Session.SetString("company_id", user.company_id?.ToString() ?? string.Empty);
                HttpContext.Session.SetString("factory_id", user.factory_id?.ToString() ?? string.Empty);
                HttpContext.Session.SetString("factory_name", user.factory_name ?? string.Empty);
                HttpContext.Session.SetString("company_name", user.company_name ?? string.Empty);

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
    }
}