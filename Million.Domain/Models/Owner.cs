using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Million.Domain.Models
{
    public class Owner
    {
        [Key]
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
