using Million.Domain.Abstractions;
using Million.Domain.PropertyImages.Events;

namespace Million.Domain.PropertyImages
{
    public sealed class PropertyImage : Entity<PropertyImageId>
    {
        public PropertyImage(
            PropertyImageId id,
            IdProperty idProperty,
            Enabled enabled,
            Shared.File file
            ): base(id)
        {
            IdProperty = idProperty;
            Enabled = enabled;
            File = file;
        }

        private PropertyImage(
            IdProperty idProperty,
            Enabled enabled,
            Shared.File file
            ) 
        {
            IdProperty = idProperty;
            Enabled = enabled;
            File = file;
        }

        public IdProperty IdProperty { get; private set; }
        public Enabled Enabled { get; private set; }
        public Shared.File File { get; private set; }


        public static PropertyImage Create(
            IdProperty idProperty,
            Enabled enabled,
            Shared.File file
            )
        {
            var propertyImage = new PropertyImage(idProperty, enabled, file);
            propertyImage.RaiseDomainEvent(new PropertyImageRegisterDomainEvent());
            return propertyImage;
        }

    }
}
