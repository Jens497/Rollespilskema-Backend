using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    public class GetTemplatesHandler : IQueryHandler<GetTemplatesQuery, IEnumerable<Template>>
    {
        private readonly RoleplayingDbContext _context;

        public GetTemplatesHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Template>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
            => await _context.Templates.OrderBy(x => x.Name).ToListAsync();
    }
}
