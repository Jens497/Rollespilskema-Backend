using MediatR;
using RoleplayingSchemaBackend.Data;

namespace RoleplayingSchemaBackend.Commands
{
    public record AddUserCommand(UserDTO User) : IRequest<String>;
}
