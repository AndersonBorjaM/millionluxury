using Million.Application.Abstractions.Messaging;
using Million.Domain.Abstractions;
using Million.Domain.Properties;

namespace Million.Application.Properties.ChangePrice
{
    internal sealed class ChangePriceCommandHandler : ICommandHandler<ChangePriceCommand, string>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePriceCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            this._propertyRepository = propertyRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
        {
            if (!(await _propertyRepository.IsPropertyExistsAsync(new PropertyId(request.IdProperty), cancellationToken)))
                return Result.Failure<string>(PropertyErrors.NotFound);

            var property = await _propertyRepository.GetByIdAsync(new PropertyId(request.IdProperty), cancellationToken);

            property.ChangePrice(new Price(request.NewPrice));

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(property.Id!.Value.ToString());
        }
    }
}
