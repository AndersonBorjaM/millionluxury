using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Repository.Repositories
{
    public class PropertyImageRepository : BaseRepository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(MillionContext context) : base(context)
        {
        }

    }
}
