using Microsoft.AspNetCore.Mvc;
using Million.Domain.DTO;
using Million.Domain.Services;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            this._propertyService = propertyService;
        }

        [HttpPost("CreateProperty")]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyDTO property) => Ok(await _propertyService.CreateProperty(property));

    }
}
