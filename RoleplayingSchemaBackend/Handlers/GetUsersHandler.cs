using MediatR;
using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Queries;

namespace RoleplayingSchemaBackend.Handlers
{
    //public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<Users>>
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<Users>>
    {
        //private readonly IQueryable<Users> _users;
        private readonly RoleplayingDbContext _context;

        //public GetUsersHandler(UserData userData)
        //  => _userData = userData;
        public GetUsersHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        /*public async Task<IEnumerable<User>> Handle(GetUsersQuery request,CancellationToken cancellationToken) 
            => await _userData.GetAllUsers();*/

        //This should return the list of users from the database rather than from the dummy setup
        public async Task<IEnumerable<Users>> Handle(GetUsersQuery request, CancellationToken cancellation)
            => await _context.Users.OrderBy(x => x.Username).ToListAsync();
    }
}
