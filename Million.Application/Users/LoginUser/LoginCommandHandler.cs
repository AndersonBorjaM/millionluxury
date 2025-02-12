using Million.Application.Abstractions.Authentication;
using Million.Application.Abstractions.Messaging;
using Million.Domain.Abstractions;
using Million.Domain.Users;

namespace Million.Application.Users.LoginUser
{
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            this._userRepository = userRepository;
            this._jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserNameAsync(new UserName(request.UserName), cancellationToken);

            if (user == null)
                return Result.Failure<string>(UserErrors.NotFound);

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash!.Value))
                return Result.Failure<string>(UserErrors.InvalidCredentials);

            return await _jwtProvider.GenerateToken(user);
        }
    }
}
