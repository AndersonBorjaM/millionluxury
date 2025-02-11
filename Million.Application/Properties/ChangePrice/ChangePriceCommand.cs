using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Million.Application.Abstractions.Messaging;

namespace Million.Application.Properties.ChangePrice
{
    public sealed record ChangePriceCommand(decimal NewPrice, int IdProperty): ICommand<string>;
}
