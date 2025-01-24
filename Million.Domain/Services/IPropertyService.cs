using Million.Domain.DTO;
using Million.Domain.Models;

namespace Million.Domain.Services
{
    public interface IPropertyService 
    {
        Task<Property> CreateProperty(PropertyDTO property);
    }
}
