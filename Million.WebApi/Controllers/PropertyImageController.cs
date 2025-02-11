using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.Application.PropertyImages.CreatePropertyImage;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyImageController : ControllerBase
    {
        private readonly ISender _sender;

        public PropertyImageController(ISender sender)
        {
            this._sender = sender;
        }

        /// <summary>
        /// Método para agregar una nueva imagen a la propiedad.
        /// </summary>
        /// <param name="propertyImage">Información de la imagen</param>
        /// <returns>Información de la imagen registrada.</returns>
        [HttpPost("CreatePropertyImage")]
        [Authorize]
        public async Task<IActionResult> CreatePropertyImageAsync([FromForm] CreatePropertyImageRequest propertyImage, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new CreatePropertyImageCommand(
                propertyImage.IdProperty,
                propertyImage.Enabled,
                propertyImage.File
                ), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);

        }
    }
}
