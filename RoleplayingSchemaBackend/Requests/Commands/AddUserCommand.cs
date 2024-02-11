using MediatR;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;

namespace RoleplayingSchemaBackend.Commands
{
    [AllowAnonymous]
    public record AddUserCommand(UserDTO User) : ICommand<String>;
}
