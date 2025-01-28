using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Million.Domain.Models
{
    public class PropertyTrace
    {
        [Key]
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Tax { get; set; }
        public int IdProperty { get; set; }
        [JsonIgnore]
        public virtual Property Property { get; set; }

    }
}
