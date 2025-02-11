using Microsoft.EntityFrameworkCore;
using Million.Domain.Abstractions;

namespace Million.Infrastructure.Abstractions.Base
{
    internal abstract class BaseRepository<TEntity, TEntityId>
        where TEntity : Entity<TEntityId> where TEntityId : class
    {
        protected readonly DbContext _context;
        protected DbSet<TEntity> _table;

        protected BaseRepository(DbContext applicationContext)
        {
            _table = applicationContext.Set<TEntity>();
            _context = applicationContext;
        }

        /// <summary>
        /// Método para realizar un nuevo registro en una tabla de base de datos.
        /// </summary>
        /// <param name="entity">Información del objeto que se va a registrar en base de datos.</param>
        /// <returns>Información del objeto registrado en base de datos.</returns>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var resp = await _table.AddAsync(entity);
            return resp.Entity;
        }

        /// <summary>
        /// Método para eliminar un registro de base de datos.
        /// </summary>
        /// <param name="entity">Información del registro a eliminar</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            _table.Remove(entity);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Método para consultar todos los registros de una tabla de base de datos.
        /// </summary>
        /// <returns>Listado de registros.</returns>
        public virtual async Task<IQueryable<TEntity>> GetAllAsync()
        => await Task.FromResult<IQueryable<TEntity>>(_table);

        /// <summary>
        /// Método para actualizar un registro que ya existe en base de datos.
        /// </summary>
        /// <param name="entity">Información del registro modificada.</param>
        /// <returns>Información modificada.</returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var resp = _table.Update(entity);
            return await Task.FromResult(resp.Entity);
        }

        /// <summary>
        /// Método para buscar un registro por el ID
        /// </summary>
        /// <param name="id">id entidad</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Información de la entidad</returns>
        public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
        => await _table.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    }
}
