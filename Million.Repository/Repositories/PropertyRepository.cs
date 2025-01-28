using Microsoft.EntityFrameworkCore;
using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Repository.Base;
using Million.Repository.Database;

namespace Million.Repository.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(MillionContext context): base(context)
        {
        }

        /// <summary>
        /// Método para consultar las propiedades por el id de la propiedad.
        /// </summary>
        /// <param name="id">id propiedad</param>
        /// <returns>Información de la propiedad o null si la propiedad no existe.</returns>
        public async Task<Property?> FindByIdAsync(int id) => await _table.FirstOrDefaultAsync(x => x.IdProperty == id);

        /// <summary>
        /// Método para consultar las propiedades por el codigo interno que tiene la propiedad.
        /// </summary>
        /// <param name="code">Codigo</param>
        /// <returns>Información de la propiedad o null si la propiedad no existe.</returns>
        public async Task<Property?> FindByCodeInternalAsync(string code) => await _table.FirstOrDefaultAsync(x => x.CodeInternal == code); 

    }
}
