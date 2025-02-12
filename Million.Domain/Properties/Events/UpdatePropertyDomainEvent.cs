using Million.Domain.Abstractions;

namespace Million.Domain.Properties.Events
{
    public  record UpdatePropertyDomainEvent(PropertyId Id): IDomainEvent;
}
