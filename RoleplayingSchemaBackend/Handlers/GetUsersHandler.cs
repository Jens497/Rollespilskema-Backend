using MediatR;
using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Queries;

namespace RoleplayingSchemaBackend.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<Users>>
    {
        private readonly UserData _userData;
        private readonly IQueryable<Users> _users;

        //public GetUsersHandler(UserData userData) => _userData = userData;
        public GetUsersHandler(IQueryable<Users> users)
            => _users = users;
        
        /*public async Task<IEnumerable<User>> Handle(GetUsersQuery request,CancellationToken cancellationToken) 
            => await _userData.GetAllUsers();*/

        //This should return the list of users from the database rather than from the dummy setup
        public async Task<IEnumerable<Users>> Handle(GetUsersQuery request, CancellationToken cancellation)
            => await _users.OrderBy(x => x.Name).ToListAsync();
    }
}
