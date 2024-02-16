using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class DeleteTemplateHandler : ICommandHandler<DeleteTemplateCommand, int>
    {
        private readonly RoleplayingDbContext _context;

        public DeleteTemplateHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteTemplateCommand request, CancellationToken cancellation)
        {
            var templates = _context.Templates.Where(t => t.TemplateId.ToString() == request.templateId).Include(x => x.Components);

            if (templates != null)
            {
                var template = templates.First();
                foreach (Component comp in template.Components)
                {
                    var component = _context.Components.First(x => x.ComponentId == comp.ComponentId);
                    _context.Components.Remove(component);
                }

                _context.Templates.Remove(template);

                return await _context.SaveChangesAsync();
            }

            throw new Exception("The template looked for was not found and can not be deleted");
        }
    }
}
