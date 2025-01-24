namespace Million.Domain.DTO
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ConfirmPasswordHash { get; set; } = string.Empty;
    }
}
