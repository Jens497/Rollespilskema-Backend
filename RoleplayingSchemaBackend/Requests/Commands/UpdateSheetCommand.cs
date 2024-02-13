using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    [Authorized]
    public record UpdateSheetCommand(Sheet sheet) : ICommand<string>;
}
