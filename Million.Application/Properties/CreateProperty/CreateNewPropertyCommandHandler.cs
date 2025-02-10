using Million.Application.Abstractions.Messaging;
using Million.Domain.Abstractions;
using Million.Domain.Owners;
using Million.Domain.Properties;

namespace Million.Application.Properties.CreateProperty
{
    internal sealed class CreateNewPropertyCommandHandler : ICommandHandler<CreateNewPropertyCommand, string>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewPropertyCommandHandler(
            IOwnerRepository ownerRepository,
            IPropertyRepository propertyRepository,
            IUnitOfWork unitOfWork
            )
        {
            this._ownerRepository = ownerRepository;
            this._propertyRepository = propertyRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateNewPropertyCommand request, CancellationToken cancellationToken)
        {
            var ownerExists = await _ownerRepository.IsOwnerExistsAsync(new OwnerId(request.Owner.IdOwner), cancellationToken);

            OwnerId ownerId = new OwnerId(request.Owner.IdOwner);

            if (!ownerExists)
            {
               var ownerResult = await _ownerRepository.CreateAsync(Owner.Create(
                       new Domain.Shared.Name(request.Owner.Name!),
                       request.Owner.Birthday!.Value,
                       new Domain.Shared.Address(request.Owner.Address!),
                       new Photo(new byte[0])
                       ));
                await _unitOfWork.SaveChangesAsync();

                ownerId = ownerResult.Id!;
            }

            var property = Property.Create(
                new Domain.Shared.Name(request.Property.Name),
                new Domain.Shared.Address(request.Property.Address),
                new Price(request.Property.Price),
                new Year(request.Property.Year),
                new CodeInternal(request.Property.Year),
                ownerId
                );


            var propertyResult = await _propertyRepository.CreateAsync(property);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success<string>(property.Id!.Value.ToString());
        }
    }
}
