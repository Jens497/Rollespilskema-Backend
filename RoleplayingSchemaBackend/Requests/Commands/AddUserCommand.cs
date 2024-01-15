using MediatR;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;

namespace RoleplayingSchemaBackend.Commands
{
    public record AddUserCommand(UserDTO User) : ICommand<String>;
}
