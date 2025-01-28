using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Million.Domain.Models
{
    public class Property
    {
        [Key]
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public int IdOwner { get; set; }

        [JsonIgnore]
        public virtual Owner Owner { get; set; }

        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; }

    }
}
