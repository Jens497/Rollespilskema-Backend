using FluentValidation;
using RoleplayingSchemaBackend.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class AddUserValidation : AbstractValidator<AddUserCommand>
    {
        public AddUserValidation()
        {
            RuleFor(x => x.User.FirstName).NotEmpty().WithMessage("Please fill in the First name.");
            RuleFor(x => x.User.UserName).NotEmpty().WithMessage("Please fill in the username.");
            RuleFor(x => x.User.Password).NotEmpty(); //Dont need a message, this is handled by the Identity
        }
    }
}
