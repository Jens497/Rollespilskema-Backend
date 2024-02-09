using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Middleware;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Queries
{
    //public record GetUsersQuery() : IRequest<IEnumerable<User>>;
    [Authorized]
    public record GetUsersQuery() : IQuery<IEnumerable<Users>>;
}
