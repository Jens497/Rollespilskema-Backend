using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;
using System.Data;
using System.Windows.Input;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    [Authorized(Roles = "Admin")]
    public record DeleteTemplateCommand(string templateId) : ICommand<int>;
}
