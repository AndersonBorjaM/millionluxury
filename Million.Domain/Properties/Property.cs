using Million.Domain.Abstractions;
using Million.Domain.Shared;

namespace Million.Domain.Properties
{
    public sealed class Property : Entity<PropertyId>
    {
        private Property(
            PropertyId id,
            Name name,
            Address address,
            Price price,
            Year year,
            CodeInternal codeInternal,
            IdOwner idOwner
            ) : base(id) 
        {
            Id = id;
            Name = name;
            Address = address;
            Price = price;
            Year = year;
            CodeInternal = codeInternal;
            IdOwner = idOwner;
        }

        public Name? Name { get; private set; }
        public Address? Address { get; private set; }
        public Price? Price { get; private set; }
        public Year? Year { get; private set; }
        public CodeInternal? CodeInternal { get; private set; }
        public IdOwner? IdOwner { get; private set; }

    }
}
