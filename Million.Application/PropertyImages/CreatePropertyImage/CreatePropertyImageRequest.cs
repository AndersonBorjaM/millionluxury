using Microsoft.AspNetCore.Http;

namespace Million.Application.PropertyImages.CreatePropertyImage
{
    public record class CreatePropertyImageRequest(
        int IdProperty,
        IFormFile? FileProperty,
        bool Enabled = false
        );
}
