using MediatR;

namespace RoleplayingSchemaBackend.Queries
{
    public record GetUsersQuery() : IRequest<IEnumerable<User>>;
}
