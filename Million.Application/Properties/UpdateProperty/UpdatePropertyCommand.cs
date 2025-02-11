using Million.Application.Abstractions.Messaging;

namespace Million.Application.Properties.UpdateProperty
{
    public record UpdatePropertyCommand(
        int PropertyId,
        string Name,
        string Address,
        decimal Price,
        string Year,
        string CodeInternal,
        int IdOwner
        ) : ICommand<string>;
}
