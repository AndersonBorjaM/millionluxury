using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {

        public OwnerController()
        {
            
        }

        /// <summary>
        /// Enpoint para registrar un nuevo propietario
        /// </summary>
        /// <param name="owner">información del propietario.</param>
        /// <returns>Información del propietario registrado.</returns>
        //[HttpPost("CreateOwner")]
        //[Authorize]
        //public async Task<IActionResult> CreateOwnerAsync([FromForm] OwnerDTO owner) => Ok(await _ownerService.CreateOwnerAsync(owner));
    }
}
