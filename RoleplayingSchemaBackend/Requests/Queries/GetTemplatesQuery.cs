﻿using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Middleware;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Requests.Queries
{
    [Authorized]
    public record GetTemplatesQuery(List<string> Ids) : IQuery<List<Template>>;
}
