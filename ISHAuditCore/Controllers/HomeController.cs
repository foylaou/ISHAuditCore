using System.Diagnostics;
using ISHAuditCore.Context;
using Microsoft.AspNetCore.Mvc;
using ISHAuditCore.Models;

namespace ISHAuditCore.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ISHAuditDbcontext dbContext, Authority authorityClass,ILogger<HomeController> logger)
        : base(dbContext, authorityClass) // 調用基類構造函數
    {
        _logger = logger;
    }

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