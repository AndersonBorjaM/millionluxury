using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTraceController : ControllerBase
    {

        public PropertyTraceController()
        {
        }

        //[HttpPost("CratePropertyTrace")]
        //[Authorize]
        //public async Task<IActionResult> CratePropertyTraceAsync(PropertyTraceDTO propertyTrace) => Ok(await _propertyTraceService.CratePropertyTraceAsync(propertyTrace));

    }
}
