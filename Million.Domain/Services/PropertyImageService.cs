using AutoMapper;
using Million.Domain.DTO;
using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Domain.Validations;

namespace Million.Domain.Services
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyImageService(IPropertyImageRepository propertyImageRepository, IPropertyRepository propertyRepository, IMapper mapper)
        {
            this._propertyImageRepository = propertyImageRepository;
            this._propertyRepository = propertyRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Método para registrar una imagen de una propiedad.
        /// </summary>
        /// <param name="propertyImage">Información de la imagen a registrar.</param>
        /// <returns>Información de la imagen que fue registada</returns>
        /// <exception cref="ArgumentException">Mensajes de error de las validaciones de las propiedades del objeto</exception>
        public async Task<PropertyImage> CreatePropertyImageAsync(PropertyImageDTO propertyImage)
        {
            if ((await _propertyRepository.FindByIdAsync(propertyImage.IdProperty)) is null)
                throw new ArgumentNullException("The property was not found for the ID provided.");

            CreatePropertyImageValidation validationRules = new();
            var resultValidation = validationRules.Validate(propertyImage);

            if (resultValidation == null || !resultValidation.IsValid)
                throw new ArgumentException(resultValidation?.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));

            return await _propertyImageRepository.CreateAsync(_mapper.Map<PropertyImage>(propertyImage));

        }

    }
}
