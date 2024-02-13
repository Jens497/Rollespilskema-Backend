using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    public class GetSheetsIdNameHandler : IQueryHandler<GetSheetsIdNameQuery, List<SheetDTO>>
    {
        private readonly RoleplayingDbContext _context;

        public GetSheetsIdNameHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<List<SheetDTO>> Handle(GetSheetsIdNameQuery request, CancellationToken cancellationToken)
        {
            return await _context.Sheets.Select(x => new SheetDTO
            {
                SheetId = x.SheetId.ToString(),
                Name = x.Name,
                TemplateId = x.TemplateId.ToString()
            }).ToListAsync();
        }
    }
}
