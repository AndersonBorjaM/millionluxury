namespace Million.Domain.PropertyImages
{
    public interface IPropertyImageRepository
    {
        Task<PropertyImage> CreateAsync(PropertyImage entity);
    }
}
