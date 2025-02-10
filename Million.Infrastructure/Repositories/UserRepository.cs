using Microsoft.EntityFrameworkCore;
using Million.Domain.Users;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Infrastructure.Repositories
{
    internal sealed class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        public UserRepository(MillionContext context): base(context)
        {
        }

        public async Task<User?> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken)
        => await _table.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);

    }
}
