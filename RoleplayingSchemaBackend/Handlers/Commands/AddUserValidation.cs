using FluentValidation;
using RoleplayingSchemaBackend.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class AddUserValidation : AbstractValidator<AddUserCommand>
    {
        public AddUserValidation()
        {
            RuleFor(x => x.User.FirstName).NotNull().NotEmpty().WithMessage("Please fill in the First name.");
            RuleFor(x => x.User.UserName).NotNull().NotEmpty().WithMessage("Please fill in the username.");
            RuleFor(x => x.User.Password).NotNull().NotEmpty().WithMessage("Password is required.");
        }
    }
}
