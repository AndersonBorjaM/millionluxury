using Million.Domain.DTO;
using Million.Domain.Models;

namespace Million.Domain.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(RegisterUserDTO user);
        Task<string> LoginUser(LoginUserDTO userDto);
    }
}
