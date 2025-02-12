using Million.Domain.Abstractions;
using Million.Domain.Owners.Events;
using Million.Domain.Shared;

namespace Million.Domain.Owners
{
    public sealed class Owner: Entity<OwnerId>
    {
        private Owner()
        {
        }

        private Owner(
            Name name,
            DateTime birthday,
            Address address,
            Photo? photo
            )
        {
            Name = name;
            Birthday = birthday;
            Address = address;
            Photo = photo;
        }

        public Owner(
            OwnerId id,
            Name name,
            DateTime birthday,
            Address address,
            Photo photo
            ) : base(id) 
        {
            Name = name;
            Birthday = birthday;
            Address = address;
            Photo = photo;
        }

        public Name? Name { get; private set; }
        public DateTime? Birthday { get; private set; }
        public Address? Address { get; private set; }
        public Photo? Photo { get; private set; }


        public static Owner Create(
            Name name,
            DateTime birthday,
            Address address,
            Photo? photo
            )
        {
            var owner = new Owner(name, birthday, address, photo);

            owner.RaiseDomainEvent(new OwnerRegisterDomainEvent());
            
            return owner;

        }

    }
}
