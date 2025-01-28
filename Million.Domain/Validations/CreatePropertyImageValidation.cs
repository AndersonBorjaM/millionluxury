using FluentValidation;
using Microsoft.AspNetCore.Http;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class CreatePropertyImageValidation : AbstractValidator<PropertyImageDTO>
    {
        public CreatePropertyImageValidation()
        {

            RuleFor(x => x.FileProperty).Must(ValidateFile).WithMessage("The field File is required.");
            RuleFor(x => x.IdProperty).NotNull().GreaterThan(0).WithMessage("The field IdProperty is required.");
            RuleFor(x => x.FileProperty).Must(ValidateFormatFile).WithMessage("Only images in jpg format are allowed.");
        }

        private bool ValidateFile(IFormFile? file) => file != null && file.Length > 0;

        private bool ValidateFormatFile(IFormFile? file)
        => file != null ? file.ContentType.ToLower().Contains("jpeg") || file.ContentType.ToLower().Contains("jpg") || file.FileName.ToLower().Contains(".jpg") || file.FileName.ToLower().Contains(".jpeg") : true;



    }
}
