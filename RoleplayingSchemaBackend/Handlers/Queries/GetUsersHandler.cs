using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Queries;
using RoleplayingSchemaBackend.Requests.Interface;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    //public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<Users>>
    public class GetUsersHandler : IQueryHandler<GetUsersQuery, IEnumerable<Users>>
    {
        private readonly RoleplayingDbContext _context;

        public GetUsersHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        //This should return the list of users from the database rather than from the dummy setup
        public async Task<IEnumerable<Users>> Handle(GetUsersQuery request, CancellationToken cancellation)
            => await _context.Users.OrderBy(x => x.UserName).ToListAsync();
    }
}
