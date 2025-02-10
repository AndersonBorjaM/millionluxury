using Million.Application.Abstractions.Messaging;

namespace Million.Application.Users.LoginUser
{
    public record LoginCommand(string UserName, string Password) : ICommand<string>;
}
