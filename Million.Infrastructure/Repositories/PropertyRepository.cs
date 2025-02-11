using Microsoft.EntityFrameworkCore;
using Million.Domain.Properties;
using Million.Infrastructure.Abstractions.Base;
using Million.Repository.Database;

namespace Million.Infrastructure.Repositories
{
    internal sealed class PropertyRepository : BaseRepository<Property, PropertyId>, IPropertyRepository
    {
        public PropertyRepository(MillionContext context) : base(context)
        {
        }

        public async Task<bool> IsPropertyExistsAsync(PropertyId propertyId, CancellationToken cancellationToken = default)
            => await _table.AsNoTracking().AnyAsync(c => c.Id == propertyId, cancellationToken);

    }
}
