using Million.Domain.Abstractions;
using Million.Domain.Owners;
using Million.Domain.Properties.Events;
using Million.Domain.Shared;

namespace Million.Domain.Properties
{
    public sealed class Property : Entity<PropertyId>
    {
        public Property(
            PropertyId id,
            Name name,
            Address address,
            Price price,
            Year year,
            CodeInternal codeInternal,
            OwnerId idOwner
            ) : base(id)
        {
            Name = name;
            Address = address;
            Price = price;
            Year = year;
            CodeInternal = codeInternal;
            IdOwner = idOwner;
        }

        private Property(
            Name name,
            Address address,
            Price price,
            Year year,
            CodeInternal codeInternal,
            OwnerId idOwner
            )
        {
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
        public OwnerId? IdOwner { get; private set; }

        public static Property Create(
            Name name,
            Address address,
            Price price,
            Year year,
            CodeInternal codeInternal,
            OwnerId idOwner
            )
        {
            var property = new Property(name, address, price, year, codeInternal, idOwner);
            property.RaiseDomainEvent(new PropertyRegisterDomainEvent());

            return property;
        }

    }
}
