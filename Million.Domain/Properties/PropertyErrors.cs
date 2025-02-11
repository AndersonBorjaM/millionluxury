using Million.Domain.Abstractions;

namespace Million.Domain.Properties
{
    public static class PropertyErrors
    {
        public static Error NotFound = new(
            "Property.Found",
            "The property searched for this id does not exist"
        );
    }
}
