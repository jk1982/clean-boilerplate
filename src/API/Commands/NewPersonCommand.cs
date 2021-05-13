using API.DTO;
using FluentValidation;

namespace API.Commands
{
    public class NewPersonCommand
    {
        public string Name { get; set; }
        public PersonContactDTO Contact { get; set; }
        public string AlternatePersonName { get; set; }
        public PersonContactDTO AlternatePersonContact { get; set; }

    }

    public class NewPersonCommandValidator : AbstractValidator<NewPersonCommand>
    {
        public NewPersonCommandValidator()
        {
           // RuleFor(x => x.Name).MinimumLength(10);
        }
    }
}
