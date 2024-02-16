using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;
using System.Windows.Input;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    [Authorized(Roles ="Admin")]
    public record AddTemplateCommand(Template template) : ICommand<String>;
}
