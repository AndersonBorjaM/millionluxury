using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Repository.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(MillionContext context): base(context)
        {
        }
    }
}
