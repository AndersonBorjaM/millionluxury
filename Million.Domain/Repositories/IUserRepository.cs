using Million.Domain.Base;
using Million.Domain.Models;

namespace Million.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByUserName(string userName);
    }
}
