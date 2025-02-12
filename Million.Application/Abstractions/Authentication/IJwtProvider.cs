using Million.Domain.Users;

namespace Million.Application.Abstractions.Authentication
{
    public interface IJwtProvider
    {
        Task<string> GenerateToken(User user);
    }
}
