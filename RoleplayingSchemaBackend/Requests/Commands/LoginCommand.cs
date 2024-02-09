using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    [AllowAnonymous]
    public record LoginCommand(string username, string password) : ICommand<bool>;
}
