using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    public record AddSheetCommand(Sheet Sheet) : ICommand<string>;
}
