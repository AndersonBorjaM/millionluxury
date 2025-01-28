using AutoMapper;
using Million.Domain.DTO;
using Million.Domain.Models;
using Million.Domain.Repositories;
using Million.Domain.Validations;

namespace Million.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerService(IOwnerRepository ownerRepository, IMapper mapper)
        {
            this._ownerRepository = ownerRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Método para crear un nuevo propietario.
        /// </summary>
        /// <param name="owner">Información del propietario a crear.</param>
        /// <returns>Información del propietario creado</returns>
        /// <exception cref="ArgumentException">Mensajes de error de las validaciones de las propiedades del objeto</exception>
        public async Task<Owner> CreateOwnerAsync(OwnerDTO owner) 
        {
            CreateOwnerValidation validationRules = new();
            var resultValidation = await validationRules.ValidateAsync(owner);

            if (resultValidation == null || !resultValidation.IsValid)
                throw new ArgumentException(resultValidation?.Errors.Select(x => x.ErrorMessage).Aggregate((current, next) => $"{current}\n{next}"));

            return await _ownerRepository.CreateAsync(_mapper.Map<Owner>(owner));
        }
    }
}
