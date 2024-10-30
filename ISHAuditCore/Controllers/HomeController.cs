using System.Diagnostics;
using ISHAuditCore.Context;
using Microsoft.AspNetCore.Mvc;
using ISHAuditCore.Models;

namespace ISHAuditCore.Controllers;

public class homeController(ISHAuditDbcontext dbContext, Authority authorityClass,UserEditService _userEditService )  //ILogger<homeController> logger  Controller 中看到類似的日誌記錄設置
    : baseController(dbContext, authorityClass, userEditService)
{
    private static UserEditService userEditService;
    // private readonly ILogger<homeController> _logger = logger;
    
    public IActionResult Index()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}