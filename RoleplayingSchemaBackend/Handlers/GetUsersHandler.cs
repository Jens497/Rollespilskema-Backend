using MediatR;
using RoleplayingSchemaBackend.Queries;

namespace RoleplayingSchemaBackend.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly UserData _userData;

        public GetUsersHandler(UserData userData) => _userData = userData;

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request,CancellationToken cancellationToken) 
            => await _userData.GetAllUsers();
    }
}
