using RoleplayingSchemaBackend.Handlers.Interface;
using System.Windows.Input;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    public record DeleteTemplateComponentCommand(string templateId, string componentId) : ICommand<int>;
}
