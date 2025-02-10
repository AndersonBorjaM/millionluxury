using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Properties
{
    public interface IPropertyRepository
    {
        Task<Property> CreateAsync(Property entity);
    }
}
