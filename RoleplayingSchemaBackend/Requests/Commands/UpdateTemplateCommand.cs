using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    [Authorized(Roles = "Admin")]
    public record UpdateTemplateCommand(Template template) : ICommand<string>;
}
