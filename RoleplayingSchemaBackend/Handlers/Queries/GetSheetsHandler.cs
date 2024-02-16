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
            => await _context.Sheets.Where(x => request.Ids.Contains(x.SheetId.ToString())).Include(x => x.Components).ToListAsync();
    }
}
