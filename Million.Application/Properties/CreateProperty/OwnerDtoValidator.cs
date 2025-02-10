using FluentValidation;

namespace Million.Application.Properties.CreateProperty
{
    public class OwnerDtoValidator : AbstractValidator<OwnerDto>
    {
        public OwnerDtoValidator()
        {
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("The field Address is required.");
            RuleFor(x => x.Birthday).Must(BeAValidDate).WithMessage("The field Birthday is required.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The field Name is required.");
        }
        private bool BeAValidDate(DateTime? date)
           => !date.Equals(default(DateTime));
    }
}
