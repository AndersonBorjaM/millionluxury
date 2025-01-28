using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Million.Domain.DTO;
using Million.Domain.Helpers;
using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Domain.Validations;

namespace Million.Domain.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper)
        {
            this._propertyRepository = propertyRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Método para registrar una propiedad.
        /// </summary>
        /// <param name="property">Información de la propiedad.</param>
        /// <returns>Información de la propiedad registrada</returns>
        /// <exception cref="ArgumentException">Mensajes de error de las validaciones de las propiedades del objeto.</exception>
        public async Task<Property> CreatePropertyAsync(PropertyDTO property)
        {
            ValidationResult resultValidation = new();

            CreatePropertyValidation propertyValidation = new();
            resultValidation = propertyValidation.Validate(property);

            if (!resultValidation.IsValid)
                throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));

            if (property.Owner != null)
            {
                CreateOwnerValidation ownerValidation = new();
                resultValidation = ownerValidation.Validate(property.Owner);

                if (!resultValidation.IsValid)
                    throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));
            }

            if (property.PropertyTraces.Count() != 0)
            {
                CreatePropertyTraceValidation propertyTraceValidation = new();


                foreach (var item in property.PropertyTraces)
                {
                    resultValidation = propertyTraceValidation.Validate(item);
                    if (!resultValidation.IsValid)
                        throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));
                }
            }

            return await _propertyRepository.CreateAsync(_mapper.Map<Property>(property));
        }

        /// <summary>
        /// Método para cambiar el precio de una propiedad buscandola por su ID.
        /// </summary>
        /// <param name="changePrice">Información del precio y el ID de la propiedad</param>
        /// <returns>Información modificada</returns>
        /// <exception cref="ArgumentNullException">Mensaje de error en caso de no encontrar la propiedad en base de datos o que el valor del precio no se valido.</exception>
        public async Task<Property> ChangePricePropertyByIdAsync(ChangePricePropertyDTO changePrice)
        {

            Property? property = changePrice.IdProperty.HasValue ? await _propertyRepository.FindByIdAsync(changePrice.IdProperty.Value) : null;

            if (property is null || !changePrice.NewPrice.IsValid())
                throw new ArgumentNullException("An error occurred when editing the price of the property.");

            property.Price = changePrice.NewPrice;

            return await _propertyRepository.UpdateAsync(property);
        }

        /// <summary>
        /// Método para cambiar el precio de una propiedad, buscando la propiedad por el codigo interno.
        /// </summary>
        /// <param name="changePrice">Información del nuevo precio y el codigo interno de la propiedad.</param>
        /// <returns>Información de la propiedad modificada.</returns>
        /// <exception cref="ArgumentNullException">Mensaje de error en caso de no encontrar la propiedad en base de datos o que el valor del precio no se valido.</exception>
        public async Task<Property> ChangePricePropertyByCodeInternalAsync(ChangePricePropertyDTO changePrice)
        {
            Property? property = await _propertyRepository.FindByCodeInternalAsync(changePrice.CodeInternal);

            if (property is null || !changePrice.NewPrice.IsValid())
                throw new ArgumentNullException("An error occurred when editing the price of the property.");

            property.Price = changePrice.NewPrice;

            return await _propertyRepository.UpdateAsync(property);
        }

        /// <summary>
        /// Método para modificar la información de una propiedad.
        /// </summary>
        /// <param name="updateProperty">Información a modificar</param>
        /// <returns>Información modificada.</returns>
        /// <exception cref="ArgumentNullException">Mensaje de error en caso de no encontrar la propiedad en base de datos.</exception>
        /// <exception cref="ArgumentException">Mensaje de error en caso de que alguno de los datos no sean validos.</exception>
        public async Task<Property> UpdatePropertyAsync(UpdatePropertyDTO updateProperty)
        {
            Property? property = await _propertyRepository.FindByIdAsync(updateProperty.IdProperty);

            if (property is null)
                throw new ArgumentNullException("An error occurred when editing the price of the property.");
            
            var resultValidation = new UpdatePropertyValidation().Validate(updateProperty);

            if (!resultValidation.IsValid)
                throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));


            property.Price = updateProperty.Price;
            property.Address = updateProperty.Address;
            property.Year = updateProperty.Year;
            property.CodeInternal = updateProperty.CodeInternal;
            property.Name = updateProperty.Name;


            return await _propertyRepository.UpdateAsync(property);
        }

        /// <summary>
        /// Método para consultar todas las propiedades y poder filtrar la información.
        /// </summary>
        /// <param name="filters">Informacion de los filtros a aplicar en la consulta.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Property>> GetPropertiesAsync(FiltersDTO filters)
            => (await _propertyRepository.GetAllAsync()).ApplyFilter(filters);

    }
}
