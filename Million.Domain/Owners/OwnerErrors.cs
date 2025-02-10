using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Million.Domain.Abstractions;

namespace Million.Domain.Owners
{
    public static class OwnerErrors
    {
        public static Error NotFound = new(
        "User.Found",
        "The owner searched for this id does not exist"
    );

    }
}
