using Microsoft.AspNetCore.Identity;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class LoginHandler : ICommandHandler<LoginCommand, bool>
    {
        private SignInManager<Users> _signInManager;
        public LoginHandler(SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LoginCommand command, CancellationToken token)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(command.username, command.password, false, false);
            return result.Succeeded;
        }
    }
}
