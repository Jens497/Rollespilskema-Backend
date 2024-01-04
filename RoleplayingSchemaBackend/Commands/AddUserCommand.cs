using MediatR;
using RoleplayingSchemaBackend.Data;

namespace RoleplayingSchemaBackend.Commands
{
    public record AddUserCommand(Users User) : IRequest;
}
