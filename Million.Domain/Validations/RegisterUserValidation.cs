using FluentValidation;
using Million.Domain.DTO;

namespace Million.Domain.Validations
{
    public class RegisterUserValidation : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidation()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("The value entered in the user field is not correct.");
            RuleFor(x => x).Must(PasswordValidate).WithMessage("The passwords do not match");
        }

        private bool PasswordValidate(RegisterUserDTO user)
        => !(string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.ConfirmPasswordHash)) ?
             user.PasswordHash.Equals(user.ConfirmPasswordHash): false;
        
    }
}
