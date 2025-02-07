using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyImageController : ControllerBase
    {

        public PropertyImageController()
        {
        }

        /// <summary>
        /// Método para agregar una nueva imagen a la propiedad.
        /// </summary>
        /// <param name="propertyImage">Información de la imagen</param>
        /// <returns>Información de la imagen registrada.</returns>
        //[HttpPost("CreatePropertyImage")]
        //[Authorize]
        //public async Task<IActionResult> CreatePropertyImageAsync([FromForm] PropertyImageDTO propertyImage) 
        //    => Ok(await _propertyImageService.CreatePropertyImageAsync(propertyImage));
    }
}
