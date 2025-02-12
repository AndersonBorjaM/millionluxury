using Million.Domain.Abstractions;

namespace Million.Domain.Properties
{
    public interface IPropertyRepository
    {
        Task<Property> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default);
        Task<Property> CreateAsync(Property entity);
        Task<bool> IsPropertyExistsAsync(PropertyId propertyId, CancellationToken cancellationToken = default);
        Task<IQueryable<Property>> GetAsync(BaseSpecification<Property>? specification = null);
    }
}
