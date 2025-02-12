using Million.Domain.PropertyImages;
using Million.Infrastructure.Abstractions.Base;
using Million.Repository.Database;

namespace Million.Infrastructure.Repositories
{
    internal sealed class PropertyImageRepository : BaseRepository<PropertyImage, PropertyImageId>, IPropertyImageRepository
    {
        public PropertyImageRepository(MillionContext context) : base(context)
        {

        }
    }
}
