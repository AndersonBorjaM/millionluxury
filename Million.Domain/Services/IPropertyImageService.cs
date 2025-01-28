using Million.Domain.DTO;
using Million.Domain.Models;

namespace Million.Domain.Services
{
    public interface IPropertyImageService
    {
        Task<PropertyImage> CreatePropertyImageAsync(PropertyImageDTO propertyImage);
    }
}
