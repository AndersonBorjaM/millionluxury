using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.DTO
{
    public class PropertyTraceDTO
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Tax { get; set; }
        public int IdProperty { get; set; }

    }
}
