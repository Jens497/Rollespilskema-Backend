using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Requests.Queries
{
    public record GetUserInfoQuery : IQuery<UserResponseDTO>;
}
