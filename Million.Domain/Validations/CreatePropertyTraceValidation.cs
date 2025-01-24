using FluentValidation;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class CreatePropertyTraceValidation : AbstractValidator<PropertyTraceDTO>
    {
        public CreatePropertyTraceValidation() 
        {
            RuleFor(x => x.IdProperty).NotNull().GreaterThan(0).WithMessage("The field IdProperty is required.");
            RuleFor(x => x.Tax).NotNull().NotEmpty().WithMessage("The Tax IdProperty is required.");
            RuleFor(x => x.Value).NotNull().GreaterThan(0).WithMessage("The field Value is required.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The field Name is required.");
            RuleFor(x => x.DateSale).Must(BeAValidDate).WithMessage("The field DateSale is required.");
        }
        private bool BeAValidDate(DateTime date)
           => !date.Equals(default(DateTime));
    }
}
