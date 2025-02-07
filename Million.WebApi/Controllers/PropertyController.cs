using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Million.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {

        public PropertyController()
        {
        }

        /// <summary>
        /// Método para registrar una propiedad.
        /// </summary>
        /// <param name="property">Información de la propiedad.</param>
        /// <returns>Información de la propiedad registrada.</returns>
        //[HttpPost("CreateProperty")]
        //[Authorize]
        //public async Task<IActionResult> CreateProperty([FromBody] PropertyDTO property) => Ok(await _propertyService.CreatePropertyAsync(property));

        /// <summary>
        /// Método para cambiar el precio de una propiedad basado en el ID de la propiedad.
        /// </summary>
        /// <param name="changePrice">ID Propiedad y nuevo precio.</param>
        /// <returns>Información de la propiedad modificada.</returns>
        //[HttpPatch("ChangePricePropertyById")]
        //[Authorize]
        //public async Task<IActionResult> ChangePricePropertyByIdAsync([FromBody] ChangePricePropertyDTO changePrice) => Ok(await _propertyService.ChangePricePropertyByIdAsync(changePrice));


        /// <summary>
        /// Método para cambiar el precio de una propiedad basado en el codigo interno de la propiedad.
        /// </summary>
        /// <param name="changePrice">Codigo de la propiedad y nuevo precio</param>
        /// <returns>Información de la propiedad modificada</returns>
        //[HttpPatch("ChangePricePropertyByCodeInternal")]
        //[Authorize]
        //public async Task<IActionResult> ChangePricePropertyByCodeInternalAsync([FromBody] ChangePricePropertyDTO changePrice) => Ok(await _propertyService.ChangePricePropertyByCodeInternalAsync(changePrice));

        /// <summary>
        /// Método para consultar las propiedades y filtrarlas.
        /// </summary>
        /// <param name="filters">Información de los filtros a aplicar.</param>
        /// <returns>Listado de propiedades.</returns>
        //[HttpPost("GetPropertiesWithFilter")]
        //[Authorize]
        //public async Task<IActionResult> GetPropertiesAsync([FromBody] FiltersDTO filters) => Ok(await _propertyService.GetPropertiesAsync(filters));

        /// <summary>
        /// Método para modificar una propiedad.
        /// </summary>
        /// <param name="updateProperty">Información de la propiedad.</param>
        /// <returns>Información modificada.</returns>
        //[HttpPatch("UpdateProperty")]
        //[Authorize]
        //public async Task<IActionResult> UpdatePropertyAsync([FromBody] UpdatePropertyDTO updateProperty) => Ok(await _propertyService.UpdatePropertyAsync(updateProperty));

    }
}
