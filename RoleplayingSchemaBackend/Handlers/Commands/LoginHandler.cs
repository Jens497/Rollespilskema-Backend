using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;
using System.Security.Claims;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class LoginHandler : ICommandHandler<LoginCommand, bool>
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

        public async Task<bool> Handle(LoginCommand command, CancellationToken token)
        {
            /*var dsa = await _userManager.FindByIdAsync("ebd52763-5773-4159-9648-feb96c82f874");
            var userRes = await _userManager.FindByNameAsync("teak");
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, command.username));
            identity.AddClaim(new Claim(ClaimTypes.Name, command.username));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, userRes.Firstname));
            foreach (var role in _userManager.GetRolesAsync(userRes).Result)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

            return true;*/

            SignInResult result = await _signInManager.PasswordSignInAsync(command.username, command.password, false, false);
            Console.WriteLine(result.ToString());
            return result.Succeeded;
        }
    }
}
