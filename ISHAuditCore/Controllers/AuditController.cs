using ISHAuditCore.Context;
using ISHAuditCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISHAuditCore.Controllers;

public class AuditController : baseController
{
    private readonly UserEditService _userEditService;
    private readonly Audit _audit;
    private readonly ISHAuditDbcontext _db;
    
    public AuditController(ISHAuditDbcontext dbContext, Authority authorityClass,Audit audit, UserEditService userEditService)
        : base(dbContext, authorityClass,userEditService) // 呼叫 baseController 的構造函數
    {
        _db = dbContext;
        _userEditService = userEditService ?? throw new ArgumentNullException(nameof(userEditService));
        _audit = audit ?? throw new ArgumentNullException(nameof(audit));
    }
    // GET
    public IActionResult Index()
    {
        var CityList = _userEditService.GetCityList();
        ViewBag.CityListJson = JsonConvert.SerializeObject(CityList);
        var company = _userEditService.Company();
        ViewBag.CompanyJson = JsonConvert.SerializeObject(company);
        var auditType = _audit.AuditType();
        ViewBag.AuditTypeJson = JsonConvert.SerializeObject(auditType);
        var auditCause = _audit.AuditCause();
        ViewBag.AuditCauseJson = JsonConvert.SerializeObject(auditCause);
        var disasterType = _audit.DisasterType();
        ViewBag.DisasterTypeJson = JsonConvert.SerializeObject(disasterType);
        
        return View();
    }
}