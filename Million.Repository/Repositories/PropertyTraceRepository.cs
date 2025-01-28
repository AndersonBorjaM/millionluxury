using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Repository.Repositories
{
    public class PropertyTraceRepository : BaseRepository<PropertyTrace>, IPropertyTraceRepository
    {
        public PropertyTraceRepository(MillionContext context) : base(context)
        {
        }

    }
}
