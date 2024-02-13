using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Queries;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    /*public class GetTemplateHandler : IQueryHandler<GetTemplateQuery, Template>
    {
        private readonly RoleplayingDbContext _context;

        public GetTemplateHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<Template> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
            => await _context.Templates.First(x => x.TemplateId == request.id);
    }*/
}
