using FluentValidation;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class CreatePropertyTraceValidation : AbstractValidator<PropertyTraceDTO>
    {
        public CreatePropertyTraceValidation() 
        {
            RuleFor(x => x.Tax).NotNull().NotEmpty().WithMessage("The Tax IdProperty is required.");
            RuleFor(x => x.Tax).NotNull().MaximumLength(250).WithMessage("The maximum length of the Tax field is 250.");

            RuleFor(x => x.Value).NotNull().GreaterThan(0).WithMessage("The field Value is required.");
            RuleFor(x => x.Value).NotNull().PrecisionScale(12, 2, false).WithMessage("The value cannot have a value greater than 999999999 and cannot have more than two decimal places.");

            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The field Name is required.");
            RuleFor(x => x.Name).NotNull().MaximumLength(250).WithMessage("The maximum length of the Name field is 250.");

            RuleFor(x => x.DateSale).Must(BeAValidDate).WithMessage("The field DateSale is required.");
        }
        private bool BeAValidDate(DateTime? date)
           => date.HasValue && !date.Equals(default(DateTime));
    }
}
