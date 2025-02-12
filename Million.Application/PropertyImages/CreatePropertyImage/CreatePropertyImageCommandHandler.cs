using Microsoft.AspNetCore.Http;
using Million.Application.Abstractions.Messaging;
using Million.Domain.Abstractions;
using Million.Domain.Properties;
using Million.Domain.PropertyImages;

namespace Million.Application.PropertyImages.CreatePropertyImage
{
    internal sealed class CreatePropertyImageCommandHandler : ICommandHandler<CreatePropertyImageCommand, string>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository, IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            this._propertyImageRepository = propertyImageRepository;
            this._propertyRepository = propertyRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            if (!(await _propertyRepository.IsPropertyExistsAsync(new PropertyId(request.IdProperty))))
                return Result.Failure<string>(PropertyErrors.NotFound);

            var propertyImage = await _propertyImageRepository.CreateAsync(PropertyImage.Create(
                new IdProperty(request.IdProperty),
                new Enabled(request.Enabled),
                new Domain.Shared.File(ConvertToArrayBytes(request.File))
                ));

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(propertyImage.Id!.Value.ToString());
        }

        private byte[] ConvertToArrayBytes(IFormFile? file)
        {
            if (file is null)
                return new byte[0];

            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
