using Microsoft.AspNetCore.Identity;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Exceptions;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class LoginHandler : ICommandHandler<LoginCommand, UserResponseDTO>
    {
        private IHttpContextAccessor _contextAccessor;
        private SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        public LoginHandler(SignInManager<Users> signInManager, UserManager<Users> userManager, IHttpContextAccessor contextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<UserResponseDTO> Handle(LoginCommand command, CancellationToken token)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(command.username, command.password, false, false);
            if (!result.Succeeded)
            {
                var errors = new Dictionary<string, string> { };

                if (result.IsNotAllowed) { errors.Add(nameof(result.IsNotAllowed), "You are not allowed to login in at this time"); }
                if (result.RequiresTwoFactor) { errors.Add(nameof(result.RequiresTwoFactor), "Two Factor Authentication required to login"); }
                if (result.IsLockedOut) { errors.Add(nameof(result.IsLockedOut), "You are currently locked out, contact a system administrator to unlock the account"); }

                throw new IdentityModelExceptions(errors);
            }

            Users user = _userManager.Users.First(user => user.UserName == command.username);
            var roles = await _userManager.GetRolesAsync(user);

            return new UserResponseDTO()
            {
                Email = user.Email,
                FirstName = user.Firstname,
                UserName = user.UserName,
                Roles = roles
            };
        }
    }
}
