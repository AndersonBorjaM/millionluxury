using FluentValidation;
using Microsoft.AspNetCore.Http;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class CreateOwnerValidation : AbstractValidator<OwnerDTO>
    {
        public CreateOwnerValidation()
        {
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("The field Address is required.");
            RuleFor(x => x.Birthday).Must(BeAValidDate).WithMessage("The field Birthday is required.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The field Name is required.");
            RuleFor(x => x.Photo).Must(ValidateFormatPhoto).WithMessage("Only images in jpg format are allowed.");
        }

        private bool ValidateFormatPhoto(IFormFile? file)
        => file != null ? file.ContentType.ToLower().Contains("jpeg") || file.ContentType.ToLower().Contains("jpg") || file.FileName.ToLower().Contains(".jpg") || file.FileName.ToLower().Contains(".jpeg") : true;



        private bool BeAValidDate(DateTime date)
           => !date.Equals(default(DateTime));
    }
}
