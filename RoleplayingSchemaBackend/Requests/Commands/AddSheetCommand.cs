﻿using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Middleware;

namespace RoleplayingSchemaBackend.Requests.Commands
{
    [Authorized]
    public record AddSheetCommand(Sheet Sheet) : ICommand<string>;
}
