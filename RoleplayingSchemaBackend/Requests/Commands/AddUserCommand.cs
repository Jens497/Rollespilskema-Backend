using MediatR;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;

namespace RoleplayingSchemaBackend.Commands
{
    [Authorized(Roles = "Admin")]
    public record AddUserCommand(UserDTO User) : ICommand<String>;
}
