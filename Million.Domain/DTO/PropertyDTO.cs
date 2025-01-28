using System.ComponentModel.DataAnnotations;
using Million.Domain.Models;

namespace Million.Domain.DTO
{
    public class PropertyDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public int IdOwner { get; set; }

        public IEnumerable<PropertyTraceDTO>? PropertyTraces { get; set; } = new List<PropertyTraceDTO>();
        
        public OwnerDTO? Owner { get; set; }
    }
}
