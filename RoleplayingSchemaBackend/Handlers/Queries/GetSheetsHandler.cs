using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    public class GetSheetsHandler : IQueryHandler<GetSheetsQuery, IEnumerable<Sheet>>
    {
        private readonly RoleplayingDbContext _context;

        public GetSheetsHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sheet>> Handle(GetSheetsQuery request, CancellationToken cancellationToken)
            => await _context.Sheets.OrderBy(x => x.Name).ToListAsync();
    }
}
