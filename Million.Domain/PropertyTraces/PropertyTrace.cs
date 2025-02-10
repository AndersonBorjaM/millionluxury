using Million.Domain.Abstractions;
using Million.Domain.Shared;

namespace Million.Domain.PropertyTraces
{
    public sealed class PropertyTrace: Entity<IdPropertyTrace>
    {
        public PropertyTrace(
            IdPropertyTrace id,
            DateTime dateSale,
            Name name,
            Value value,
            Tax tax,
            IdProperty idProperty
            ):base(id) 
        {
            DateSale = dateSale;
            Name = name;
            Tax = tax;
            Value = value;
            IdProperty = idProperty;
        }

        public DateTime DateSale { get; private set; }
        public Name? Name { get; private set; }
        public Value? Value { get; private set; }
        public Tax? Tax { get; private set; }
        public IdProperty? IdProperty { get; private set; }
    }
}
