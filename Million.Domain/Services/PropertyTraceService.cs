using AutoMapper;
using Million.Domain.DTO;
using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Domain.Validations;

namespace Million.Domain.Services
{
    public class PropertyTraceService : IPropertyTraceService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly IMapper _mapper;

        public PropertyTraceService(IPropertyRepository propertyRepository, IPropertyTraceRepository propertyTraceRepository, IMapper mapper)
        {
            this._propertyRepository = propertyRepository;
            this._propertyTraceRepository = propertyTraceRepository;
            this._mapper = mapper;
        }

        public async Task<PropertyTrace> CratePropertyTraceAsync(PropertyTraceDTO propertyTrace)
        {
            if (!propertyTrace.IdProperty.HasValue || (await _propertyRepository.FindByIdAsync(propertyTrace.IdProperty.Value)) is null)
                throw new ArgumentNullException("The IdProperty is not valid.");

            var resultValidation = new CreatePropertyTraceValidation().Validate(propertyTrace);

            if (!resultValidation.IsValid)
                throw new ArgumentException(resultValidation.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));

            return await _propertyTraceRepository.CreateAsync(_mapper.Map<PropertyTrace>(propertyTrace));
        }

    }
}
