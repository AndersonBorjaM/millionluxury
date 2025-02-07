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

        /// <summary>
        /// Método para realizar un nuevo registro en una tabla de base de datos.
        /// </summary>
        /// <param name="entity">Información del objeto que se va a registrar en base de datos.</param>
        /// <returns>Información del objeto registrado en base de datos.</returns>
        public virtual async Task<Entity> CreateAsync(Entity entity)
        {
            var resp = await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return resp.Entity;
        }

        /// <summary>
        /// Método para eliminar un registro de base de datos.
        /// </summary>
        /// <param name="entity">Información del registro a eliminar</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(Entity entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Método para consultar todos los registros de una tabla de base de datos.
        /// </summary>
        /// <returns>Listado de registros.</returns>
        public virtual async Task<IQueryable<Entity>> GetAllAsync()
        => await Task.FromResult<IQueryable<Entity>>(_table);

        /// <summary>
        /// Método para actualizar un registro que ya existe en base de datos.
        /// </summary>
        /// <param name="entity">Información del registro modificada.</param>
        /// <returns>Información modificada.</returns>
        public virtual async Task<Entity> UpdateAsync(Entity entity)
        {
            var resp = _table.Update(entity);
            await _context.SaveChangesAsync();
            return resp.Entity;
        }
    }
}
