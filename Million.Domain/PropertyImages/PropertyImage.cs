using Million.Domain.Abstractions;

namespace Million.Domain.PropertyImages
{
    public sealed class PropertyImage : Entity<PropertyImageId>
    {
        private PropertyImage(
            PropertyImageId id,
            IdProperty idProperty,
            Enabled enabled,
            Shared.File file
            ): base(id)
        {
            Id = id;
            IdProperty = idProperty;
            Enabled = enabled;
            File = file;
        }

        public IdProperty IdProperty { get; private set; }
        public Enabled Enabled { get; private set; }
        public Shared.File File { get; private set; }
    }
}
