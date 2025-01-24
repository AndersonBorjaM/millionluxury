using FluentValidation;
using Microsoft.AspNetCore.Http;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class CreatePropertyImageValidation : AbstractValidator<PropertyImageDTO>
    {
        public CreatePropertyImageValidation()
        {

            RuleFor(x => x.File).Must(ValidateFile).WithMessage("The field File is required.");
            RuleFor(x => x.IdProperty).NotNull().GreaterThan(0).WithMessage("The field IdProperty is required.");
        }

        private bool ValidateFile(IFormFile? file) => file != null && file.Length > 0;

    }
}
