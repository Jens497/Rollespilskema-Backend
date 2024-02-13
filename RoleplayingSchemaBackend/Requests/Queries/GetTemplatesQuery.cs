﻿using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Middleware;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Requests.Queries
{
    [Authorized]
    public record GetTemplatesQuery() : IQuery<IEnumerable<Template>>;
}
