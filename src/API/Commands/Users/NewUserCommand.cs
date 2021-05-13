using FluentValidation;

namespace API.Commands.Users
{

    public class NewUserCommand
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

    }

    public class NewUserCommandValidator : AbstractValidator<NewUserCommand>
    {
        public NewUserCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.PasswordHash).MinimumLength(6).NotEmpty();
        }
    }
}
