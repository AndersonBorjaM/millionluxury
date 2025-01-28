using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class UpdatePropertyValidation : AbstractValidator<UpdatePropertyDTO>
    {
        public UpdatePropertyValidation()
        {
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("The field Address is required.");
            RuleFor(x => x.Address).NotNull().MaximumLength(500).WithMessage("The maximum length of the Address field is 500.");

            RuleFor(x => x.Price).NotNull().GreaterThan(0).WithMessage("The field Price is required.");
            RuleFor(x => x.Price).NotNull().PrecisionScale(12, 2, false).WithMessage("The price cannot have a value greater than 999999999 and cannot have more than two decimal places.");

            RuleFor(x => x.Year).NotNull().NotEmpty().WithMessage("The field Year is required.");
            RuleFor(x => x.Year).NotNull().NotEmpty().MaximumLength(4).WithMessage("The maximum length of the Year field is 4.");

            RuleFor(x => x.CodeInternal).NotNull().NotEmpty().WithMessage("The field Code Internal is required.");
            RuleFor(x => x.CodeInternal).NotNull().MaximumLength(250).WithMessage("The maximum length of the Code Internal field is 250");


            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The field Name is required.");
            RuleFor(x => x.Name).NotNull().MaximumLength(250).WithMessage("The maximum length of the Name field is 250.");

        }
    }
}
