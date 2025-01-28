using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.Domain.DTO;
using Million.Domain.Services;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IPropertyTraceService _propertyTraceService;

        public PropertyTraceController(IPropertyTraceService propertyTraceService)
        {
            this._propertyTraceService = propertyTraceService;
        }

        [HttpPost("CratePropertyTrace")]
        [Authorize]
        public async Task<IActionResult> CratePropertyTraceAsync(PropertyTraceDTO propertyTrace) => Ok(await _propertyTraceService.CratePropertyTraceAsync(propertyTrace));

    }
}
