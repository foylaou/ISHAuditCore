using ISHAuditCore.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISHAuditCore.ApiController;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class adminController(ISHAuditDbcontext dbContext)
    : ControllerBase
{
    
    [HttpGet("GetData")]
    public IActionResult GetData()
    {
        return Ok(new { Name = "John", Age = 30 });
    }
    
}