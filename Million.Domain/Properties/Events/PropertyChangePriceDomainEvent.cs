using Million.Domain.Abstractions;

namespace Million.Domain.Properties.Events
{
    public sealed record PropertyChangePriceDomainEvent(PropertyId PropertyId): IDomainEvent;
}
