using Million.Domain.Base;
using Million.Domain.Models;

namespace Million.Domain.Repositories
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        Task<Property?> FindByIdAsync(int id);
        Task<Property?> FindByCodeInternalAsync(string code);
    }
}
