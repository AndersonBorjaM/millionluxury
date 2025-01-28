using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Million.Domain.Models
{
    public class PropertyImage
    {
        [Key]
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public byte[]? FileProperty { get; set; }
        public bool Enabled { get; set; } = false;
        [JsonIgnore]
        public virtual Property Property { get; set; }
    }
}
