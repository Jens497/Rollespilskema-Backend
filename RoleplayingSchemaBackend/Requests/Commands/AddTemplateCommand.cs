using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using System.Windows.Input;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    public record AddTemplateCommand(Template template) : ICommand<String>;
}
