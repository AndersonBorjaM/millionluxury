namespace Million.Application.Properties.UpdateProperty
{
    public record UpdatePropertyRequest(
        int PropertyId,    
        string Name,
        string Address,
        decimal Price,
        string Year,
        string CodeInternal,
        int IdOwner
        );
}
