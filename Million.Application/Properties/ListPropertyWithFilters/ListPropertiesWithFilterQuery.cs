using Million.Application.Abstractions.Messaging;

namespace Million.Application.Properties.ListPropertyWithFilters
{
    public sealed record ListPropertiesWithFilterQuery(
        string? Name,
        string? Address,
        string? Year,
        string? CodeInternal
        ) : IQuery<List<ListPropertiesResponse>>
    {
    }
}
