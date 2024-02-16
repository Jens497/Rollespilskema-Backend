using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Queries;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    public class GetTemplatesIdNameHandler : IQueryHandler<GetTemplatesIdNameQuery, List<TemplateDTO>>
    {
        private readonly RoleplayingDbContext _context;

        public GetTemplatesIdNameHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<List<TemplateDTO>> Handle(GetTemplatesIdNameQuery request, CancellationToken cancellationToken)
        {
            return await _context.Templates.Select(x => new TemplateDTO
            {
                Id = x.TemplateId.ToString(),
                Name = x.Name
            }).ToListAsync();
        }
            
    }
}
