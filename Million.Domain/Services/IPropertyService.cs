using Million.Domain.DTO;
using Million.Domain.Models;

namespace Million.Domain.Services
{
    public interface IPropertyService 
    {
        Task<Property> CreatePropertyAsync(PropertyDTO property);
        Task<IEnumerable<Property>> GetPropertiesAsync(FiltersDTO filters);
        Task<Property> ChangePricePropertyByIdAsync(ChangePricePropertyDTO changePrice);
        Task<Property> ChangePricePropertyByCodeInternalAsync(ChangePricePropertyDTO changePrice);
        Task<Property> UpdatePropertyAsync(UpdatePropertyDTO updateProperty);
    }
}
