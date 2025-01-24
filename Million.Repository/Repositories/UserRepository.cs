using Microsoft.EntityFrameworkCore;
using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MillionContext context): base(context)
        {
        }

        /// <summary>
        /// Método para consultar un usuario por su nombre de usuario
        /// </summary>
        /// <param name="userName">nombre del usuario</param>
        /// <returns>Información del usuario o un null si el usuario no existe</returns>
        public async Task<User?> GetUserByUserName(string userName) => await _table.FirstOrDefaultAsync(x => x.UserName.Equals(userName));

    }
}
