using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Million.Application.PropertyImages.CreatePropertyImage
{
    public class CreatePropertyImageCommandValidator : AbstractValidator<CreatePropertyImageCommand>
    {
        public CreatePropertyImageCommandValidator()
        {
            RuleFor(x => x.File).Must(ValidateFile).WithMessage("The field File is required.")
            .Must(ValidateFormatFile).WithMessage("Only images in jpg format are allowed.");
            RuleFor(x => x.IdProperty).NotNull().GreaterThan(0).WithMessage("The field IdProperty is required.");
        }

        private bool ValidateFile(IFormFile? file) => file != null && file.Length > 0;
        private bool ValidateFormatFile(IFormFile? file)
        => file != null ? file.ContentType.ToLower().Contains("jpeg") || file.ContentType.ToLower().Contains("jpg") || file.FileName.ToLower().Contains(".jpg") || file.FileName.ToLower().Contains(".jpeg") : true;
    }
}
