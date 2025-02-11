using Microsoft.AspNetCore.Http;

namespace Million.Application.Owners.CreateOwner
{
    public record CreateOwnerRequest(int IdOwner, string Name, DateTime Birthday, string Address);
}
