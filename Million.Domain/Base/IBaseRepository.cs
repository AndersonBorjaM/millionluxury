namespace Million.Domain.Base
{
    public interface IBaseRepository<EntityDTO> where EntityDTO : class
    {
        Task<EntityDTO> CreateAsync(EntityDTO entity);
        Task<EntityDTO> UpdateAsync(EntityDTO entity);
        Task<bool> DeleteAsync(EntityDTO entity);
        Task<IQueryable<EntityDTO>> GetAllAsync();
    }
}
