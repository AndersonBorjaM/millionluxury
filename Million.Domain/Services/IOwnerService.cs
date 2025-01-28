using Million.Domain.DTO;
using Million.Domain.Models;

namespace Million.Domain.Services
{
    public interface IOwnerService
    {
        Task<Owner> CreateOwnerAsync(OwnerDTO owner);
    }
}
