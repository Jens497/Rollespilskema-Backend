using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class DeleteTemplateComponentHandler : ICommandHandler<DeleteTemplateComponentCommand, int>
    {
        private readonly RoleplayingDbContext _context;

        public DeleteTemplateComponentHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteTemplateComponentCommand request, CancellationToken cancellation)
        {
            Template? template = await _context.Templates.Where(t => t.TemplateId.ToString() == request.templateId).Include(x => x.Components).FirstOrDefaultAsync(cancellation);
            if(template == null)
            {
                throw new Exception("The template looked for was not found and can not be deleted"); 
            }
            Component? component = template.Components.Where(x => x.ComponentId.ToString() == request.componentId).FirstOrDefault();
            if (component == null)
            {
                throw new Exception("The component looked for was not found and can not be deleted");
            }

            _context.Components.Remove(component);

            return await _context.SaveChangesAsync();

        }
    }
}
