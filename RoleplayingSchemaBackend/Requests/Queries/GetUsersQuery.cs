using MediatR;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Queries
{
    //public record GetUsersQuery() : IRequest<IEnumerable<User>>;
    public record GetUsersQuery() : IQuery<IEnumerable<Users>>;
}
