using Microsoft.AspNetCore.Http;
using Million.Application.Abstractions.Messaging;

namespace Million.Application.PropertyImages.CreatePropertyImage
{
    public record CreatePropertyImageCommand(
            int IdProperty,
            bool Enabled,
            IFormFile? File) : ICommand<string>;
}
