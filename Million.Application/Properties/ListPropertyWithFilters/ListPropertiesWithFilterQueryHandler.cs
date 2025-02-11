using Million.Application.Abstractions.Messaging;
using Million.Domain.Abstractions;
using Million.Domain.Properties;
using Million.Domain.Properties.Specifications;

namespace Million.Application.Properties.ListPropertyWithFilters
{
    internal sealed class ListPropertiesWithFilterQueryHandler : IQueryHandler<ListPropertiesWithFilterQuery, List<ListPropertiesResponse>>
    {
        private readonly IPropertyRepository _propertyRepository;

        public ListPropertiesWithFilterQueryHandler(IPropertyRepository propertyRepository)
        {
            this._propertyRepository = propertyRepository;
        }

        public async Task<Result<List<ListPropertiesResponse>>> Handle(ListPropertiesWithFilterQuery request, CancellationToken cancellationToken)
        {
            BaseSpecification<Property> spec = new PropertySpecification();

            if (!string.IsNullOrEmpty(request.Address))
            {
                spec = spec.And(new PropertyAddressSpecification(new Domain.Shared.Address(request.Address)));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                spec = spec.And(new PropertyNameSpecification(new Domain.Shared.Name(request.Name)));
            }

            if (!string.IsNullOrEmpty(request.Year))
            {
                spec = spec.And(new PropertyYearSpecification(new Year(request.Year)));
            }

            if (!string.IsNullOrEmpty(request.CodeInternal))
            {
                spec = spec.And(new PropertyCodeInternalSpecification(new CodeInternal(request.CodeInternal)));
            }

            var result = await _propertyRepository.GetAsync(spec);

            return Result.Success(result.Select(x => new ListPropertiesResponse {
            Address = x.Address!.Value,
            CodeInternal = x.CodeInternal!.Value,
            IdOwner = x.IdOwner!.Value.ToString(),
            Name = x.Name!.Value,
            Price = x.Price!.Value,
            Year = x.Year!.Value
            }).ToList());

        }
    }
}
