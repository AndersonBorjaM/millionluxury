namespace Million.Application.Properties.ListPropertyWithFilters
{
    public record ListPropertiesRequest(
         string? Name,
         string? Address,
         string? Year,
         string? CodeInternal
        );
}
