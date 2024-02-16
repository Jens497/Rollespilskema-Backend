using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    [Authorized(Roles = "Admin")]
    public record AddSheetCommand(AddSheetDTO sheetDto) : ICommand<string>;
}
