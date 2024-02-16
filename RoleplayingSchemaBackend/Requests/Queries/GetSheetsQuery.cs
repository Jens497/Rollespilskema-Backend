using Microsoft.AspNetCore.Authorization;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Middleware;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Requests.Queries
{
    [Authorized]
    public record GetSheetsQuery(List<string> Ids) : IQuery<IEnumerable<Sheet>>;
}
