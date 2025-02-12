using Microsoft.AspNetCore.Http;
using Million.Application.Abstractions.Messaging;

namespace Million.Application.Properties.CreateProperty
{
    public record CreatePropertyCommand(
            PropertyDto Property,
            OwnerDto Owner
        ) : ICommand<string>;

    public class PropertyDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Year { get; set; } = string.Empty;
        public string CodeInternal { get; set; } = string.Empty;
    }

    public class OwnerDto
    {
        public int IdOwner { get; set; }
        public string? Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
    }

}
