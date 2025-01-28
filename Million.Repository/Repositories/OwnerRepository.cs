using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Repository.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(MillionContext context):base(context) { }
    }
}
