using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Middleware;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Queries
{
    [Authorized(Roles = "Admin")]
    public record GetUsersQuery() : IQuery<IEnumerable<Users>>;
}
