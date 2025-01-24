using Microsoft.EntityFrameworkCore;

namespace Million.Repository.Base
{
    public abstract class BaseRepository<Entity> where Entity : class
    {
        protected readonly DbContext _context;
        protected DbSet<Entity> _table;

        protected BaseRepository(DbContext applicationContext)
        {
            _table = applicationContext.Set<Entity>();
            _context = applicationContext;
        }

        public virtual async Task<Entity> CreateAsync(Entity entity)
        {
            var resp = await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return resp.Entity;
        }

        public virtual async Task<bool> DeleteAsync(Entity entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IQueryable<Entity>> GetAllAsync()
        => _table;

        public virtual async Task<Entity> UpdateAsync(Entity entity)
        {
            var resp = _table.Update(entity);
            await _context.SaveChangesAsync();
            return resp.Entity;
        }
    }
}
