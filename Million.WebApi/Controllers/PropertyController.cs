using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Properties.ChangePrice;
using Million.Application.Properties.CreateProperty;
using Million.Application.Properties.UpdateProperty;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ISender _sender;

        public PropertyController(ISender sender)
        {
            this._sender = sender;
        }

        /// <summary>
        /// Método para registrar una propiedad.
        /// </summary>
        /// <param name="property">Información de la propiedad.</param>
        /// <returns>Información de la propiedad registrada.</returns>
        [HttpPost("CreateProperty")]
        [Authorize]
        public async Task<IActionResult> CreateProperty([FromBody] CreateNewPropertyRequest property, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new CreateNewPropertyCommand(
                new PropertyDto
                {
                    Address = property.Address,
                    Name = property.Name,
                    CodeInternal = property.CodeInternal,
                    Price = property.Price,
                    Year = property.Year
                },
                new OwnerDto
                {
                    Address = property.Owner.Address,
                    Birthday = property.Owner.Birthday,
                    Name = property.Owner.Name,
                    IdOwner = property.Owner.IdOwner
                }
                ), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);


            return Ok(result.Value);
        }

        /// <summary>
        /// Método para cambiar el precio de una propiedad basado en el codigo interno de la propiedad.
        /// </summary>
        /// <param name="changePrice">Codigo de la propiedad y nuevo precio</param>
        /// <returns>Información de la propiedad modificada</returns>
        [HttpPatch("ChangePricePropertyById")]
        [Authorize]
        public async Task<IActionResult> ChangePricePropertyByCodeInternalAsync([FromBody] ChangePriceRequest changePrice, CancellationToken cancellationToken = default)
        {
            var result = await _sender.Send(new ChangePriceCommand(changePrice.NewPrice, changePrice.IdProperty), cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);

        }

        /// <summary>
        /// Método para modificar una propiedad.
        /// </summary>
        /// <param name="updateProperty">Información de la propiedad.</param>
        /// <returns>Información modificada.</returns>
        [HttpPatch("UpdateProperty")]
        [Authorize]
        public async Task<IActionResult> UpdatePropertyAsync([FromBody] UpdatePropertyRequest updateProperty, CancellationToken cancellationToken) 
        {
            var result = await _sender.Send(new UpdatePropertyCommand(
                updateProperty.PropertyId,
                updateProperty.Name,
                updateProperty.Address,
                updateProperty.Price,
                updateProperty.Year,
                updateProperty.CodeInternal,
                updateProperty.IdOwner
                ), cancellationToken);

            if (result.IsFailure) return NotFound(result.Error);

            return Ok(result.Value);
        }
        

        /// <summary>
        /// Método para consultar las propiedades y filtrarlas.
        /// </summary>
        /// <param name="filters">Información de los filtros a aplicar.</param>
        /// <returns>Listado de propiedades.</returns>
        //[HttpPost("GetPropertiesWithFilter")]
        //[Authorize]
        //public async Task<IActionResult> GetPropertiesAsync([FromBody] FiltersDTO filters) => Ok(await _propertyService.GetPropertiesAsync(filters));


    }
}
