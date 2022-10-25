using FluentValidation;

namespace StoreApi.Apps.AdminApp.DTOs.AccountDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(23).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).MinimumLength(5).MaximumLength(18).NotEmpty().WithMessage("UserName is required");

        }
    }
}
