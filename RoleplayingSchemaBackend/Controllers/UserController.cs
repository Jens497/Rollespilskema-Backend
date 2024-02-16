using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Commands;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Queries;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        public async Task<ActionResult> GetCurrentUserInfo()
        {
            var currentUser = await _mediator.Send(new GetUserInfoQuery());
            return Ok(currentUser);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody]UserDTO user)
        {
            var result = await _mediator.Send(new AddUserCommand(user));

            return Ok(result);
        }
    }
}
