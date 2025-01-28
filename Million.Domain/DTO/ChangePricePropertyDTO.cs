using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.DTO
{
    public class ChangePricePropertyDTO
    {
        public int? IdProperty { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public decimal NewPrice { get; set; }
    }
}
