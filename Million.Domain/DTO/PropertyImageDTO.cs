using Microsoft.AspNetCore.Http;

namespace Million.Domain.DTO
{
    public class PropertyImageDTO
    {
        public int IdProperty { get; set; }
        public IFormFile? FileProperty { get; set; }
        public bool Enabled { get; set; } = false;
    }
}
