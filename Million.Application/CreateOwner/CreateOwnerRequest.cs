using Microsoft.AspNetCore.Http;

namespace Million.Application.CreateOwner
{
    public record CreateOwnerRequest(int IdOwner, string Name, DateTime Birthday, string Address);
}
