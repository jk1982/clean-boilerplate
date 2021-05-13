using FluentValidation;

namespace API.Commands.Auth
{
    public class LoginCommand
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.PasswordHash).MinimumLength(6).NotEmpty();
        }
    }
}
