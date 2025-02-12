using FluentValidation;

namespace Million.Application.Properties.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Property.Address).NotNull().NotEmpty().WithMessage("The field Address is required.")
                .MaximumLength(500).WithMessage("The maximum length of the Address field is 500.");
            RuleFor(x => x.Property.Price).NotNull().GreaterThan(0).WithMessage("The field Price is required.")
                .PrecisionScale(12, 2, false).WithMessage("The price cannot have a value greater than 999999999 and cannot have more than two decimal places.");
            RuleFor(x => x.Property.Year).NotNull().NotEmpty().WithMessage("The field Year is required.")
                .MaximumLength(4).WithMessage("The maximum length of the Year field is 4.");
            RuleFor(x => x.Property.CodeInternal).NotNull().NotEmpty().WithMessage("The field Code Internal is required.")
                .MaximumLength(250).WithMessage("The maximum length of the Code Internal field is 250");
            RuleFor(x => x.Property.Name).NotNull().NotEmpty().WithMessage("The field Name is required.")
                .MaximumLength(250).WithMessage("The maximum length of the Name field is 250.");
            RuleFor(x => x.Owner).NotNull().SetValidator(new OwnerDtoValidator()); 
        }
    }
}
