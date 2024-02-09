using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Commands;

namespace RoleplayingSchemaBackend.Controllers
{
    [Route("api/auth/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly IMediator _mediator;

        public AuthController(SignInManager<Users> signInManager, IMediator mediator)
        {
            _signInManager = signInManager;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginCommand login)
        {
            var result = await _mediator.Send(login);

            return result ? Ok() : BadRequest();
        }

        [HttpPost]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpGet]
        public async Task<bool> IsLoggedIn()
        {
            return _signInManager.IsSignedIn(this.User);
        }
    }
}
