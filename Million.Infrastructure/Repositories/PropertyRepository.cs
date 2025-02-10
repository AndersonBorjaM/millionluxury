using Million.Domain.Properties;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Infrastructure.Repositories
{
    internal sealed class PropertyRepository : BaseRepository<Property, PropertyId>, IPropertyRepository
    {
        public PropertyRepository(MillionContext context) : base(context)
        {
        }
    }
}
