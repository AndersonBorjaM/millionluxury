using Million.Application.Abstractions.Messaging;

namespace Million.Application.Owners.CreateOwner
{
    public record CreateOwnerCommand : ICommand<string>;
}
