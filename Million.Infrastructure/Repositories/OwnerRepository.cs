using Microsoft.EntityFrameworkCore;
using Million.Domain.Owners;
using Million.Infrastructure.Abstractions.Base;
using Million.Repository.Database;

namespace Million.Infrastructure.Repositories
{
    internal sealed class OwnerRepository: BaseRepository<Owner, OwnerId>, IOwnerRepository
    {
        public OwnerRepository(MillionContext context): base(context) { }

        public async Task<bool> IsOwnerExistsAsync(OwnerId ownerId, CancellationToken cancellationToken = default)
            => await _table.AsNoTracking().AnyAsync(c => c.Id == ownerId, cancellationToken);
    }
}
