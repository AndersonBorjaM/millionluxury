using Microsoft.AspNetCore.Http;

namespace Million.Domain.DTO
{
    public class OwnerDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile? Photo { get; set; } = null;
        public DateTime Birthday { get; set; }
    }
}
