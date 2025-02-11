using Million.Application.Owners.CreateOwner;

namespace Million.Application.Properties.CreateProperty
{
    public record CreateNewPropertyRequest(
            string Name,
            string Address,
            decimal Price,
            string Year,
            string CodeInternal,
            CreateOwnerRequest Owner);
}
