using FluentValidation;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class CreatePropertyValidation : AbstractValidator<PropertyDTO>
    {
        public CreatePropertyValidation()
        {
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("The field Address is required.");
            RuleFor(x => x.Price).NotNull().GreaterThan(0).WithMessage("The field Price is required.");
            RuleFor(x => x.Year).NotNull().NotEmpty().WithMessage("The field Year is required.");
            RuleFor(x => x.CodeInternal).NotNull().NotEmpty().WithMessage("The field Code Internal is required.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The field Name is required.");
            RuleFor(x => x).Must(ValidateOwner).WithMessage("Is Required the information from Owner");
        }

        private bool ValidateOwner(PropertyDTO dTO)
        => !(dTO.Owner == null && dTO.IdOwner == 0);
    }
}
