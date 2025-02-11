using Million.Application.Abstractions.Messaging;
using Million.Domain.Abstractions;
using Million.Domain.Owners;
using Million.Domain.Properties;

namespace Million.Application.Properties.UpdateProperty
{
    internal sealed class UpdatePropertyCommandHandler : ICommandHandler<UpdatePropertyCommand, string>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository, IOwnerRepository ownerRepository, IUnitOfWork unitOfWork)
        {
            this._propertyRepository = propertyRepository;
            this._ownerRepository = ownerRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(new PropertyId(request.PropertyId), cancellationToken);

            if (property is null)
                return Result.Failure<string>(PropertyErrors.NotFound);

            if (!(await _ownerRepository.IsOwnerExistsAsync(new OwnerId(request.IdOwner))))
                return Result.Failure<string>(OwnerErrors.NotFound);

            property.UpdateProperty(
                new Domain.Shared.Name(request.Name),
                new Domain.Shared.Address(request.Address),
                new Price(request.Price),
                new Year(request.Year),
                new CodeInternal(request.CodeInternal),
                new OwnerId(request.IdOwner)
                );

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(property.Id!.Value.ToString());
        }
    }
}
