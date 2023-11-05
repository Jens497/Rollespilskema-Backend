using MediatR;
using RoleplayingSchemaBackend.Data;

namespace RoleplayingSchemaBackend.Queries
{
    //public record GetUsersQuery() : IRequest<IEnumerable<User>>;
    public record GetUsersQuery() : IRequest<IEnumerable<Users>>;
}
