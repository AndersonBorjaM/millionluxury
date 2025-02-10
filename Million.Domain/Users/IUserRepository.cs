namespace Million.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken);
    }
}
