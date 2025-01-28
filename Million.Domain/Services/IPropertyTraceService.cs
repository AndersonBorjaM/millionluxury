using Million.Domain.DTO;
using Million.Domain.Models;

namespace Million.Domain.Services
{
    public interface IPropertyTraceService
    {
        Task<PropertyTrace> CratePropertyTraceAsync(PropertyTraceDTO propertyTrace);
    }
}
