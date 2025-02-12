using Million.Application.Owners.CreateOwner;

namespace Million.Application.Properties.CreateProperty
{
    public record CreatePropertyRequest(
            string Name,
            string Address,
            decimal Price,
            string Year,
            string CodeInternal,
            CreateOwnerRequest Owner);
}
