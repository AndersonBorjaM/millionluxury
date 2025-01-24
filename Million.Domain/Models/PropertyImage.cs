using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Million.Domain.Models
{
    public class PropertyImage
    {
        [Key]
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public byte[]? File { get; set; }
        public bool Enabled { get; set; } = false;

        public virtual Property Property { get; set; }
    }
}
