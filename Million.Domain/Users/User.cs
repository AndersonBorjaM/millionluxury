using Million.Domain.Abstractions;

namespace Million.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        public User(
            UserId id,
            UserName userName,
            PasswordHash passwordHash
            ) : base(id)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public UserName? UserName { get; private set; }
        public PasswordHash? PasswordHash { get; private set; }

    }
}
