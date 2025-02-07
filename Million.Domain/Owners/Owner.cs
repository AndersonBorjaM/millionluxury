using Million.Domain.Abstractions;
using Million.Domain.Shared;

namespace Million.Domain.Owners
{
    public sealed class Owner: Entity<OwnerId>
    {
        private Owner(
            OwnerId id,
            Name name,
            DateTime birthday,
            Address address,
            Photo photo
            ) : base(id) 
        {
            Id = id;
            Name = name;
            Birthday = birthday;
            Address = address;
            Photo = photo;
        }

        public Name Name { get; private set; }
        public DateTime? Birthday { get; private set; }
        public Address? Address { get; private set; }
        public Photo? Photo { get; private set; }

    }
}
