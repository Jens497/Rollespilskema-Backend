using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    public class GetTemplatesHandler : IQueryHandler<GetTemplatesQuery, List<Template>>
    {
        private readonly RoleplayingDbContext _context;

        public GetTemplatesHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Template>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
            => await _context.Templates.Where(x => request.Ids.Contains(x.TemplateId.ToString())).Include(x => x.Components).ToListAsync();
         
    }
}
