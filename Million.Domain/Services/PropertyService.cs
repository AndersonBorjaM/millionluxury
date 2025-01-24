using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Million.Domain.DTO;
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

        public async Task<Property> CreateProperty(PropertyDTO property)
        {
            ValidationResult resultValidation = new();

            CreatePropertyValidation propertyValidation = new();
            resultValidation = propertyValidation.Validate(property);

            if (resultValidation.IsValid)
                throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}, {next}"));

            if (property.Owner != null)
            {
                CreateOwnerValidation ownerValidation = new();
                resultValidation = ownerValidation.Validate(property.Owner);

                if (resultValidation.IsValid)
                    throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}, {next}"));
            }

            if (property.PropertyTraces.Count() != 0) 
            {
                CreatePropertyTraceValidation propertyTraceValidation = new();

                var validation = property.PropertyTraces.Where(x => propertyTraceValidation.Validate(x).IsValid).Select(x => x.Name).Aggregate((current, next) => $"{current}, Is required {next}");

                if (!string.IsNullOrEmpty(validation.Replace(" ", "").Replace(",", "")))
                    throw new ArgumentException(validation);

            }

            return await _propertyRepository.CreateAsync(_mapper.Map<Property>(property));
        }
    }
}
