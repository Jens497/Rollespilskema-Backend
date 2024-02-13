using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;
using RoleplayingSchemaBackend.Exceptions;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class UpdateTemplateHandler : ICommandHandler<UpdateTemplateCommand, string>
    {
        private readonly RoleplayingDbContext _context;

        public UpdateTemplateHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            /*
                1. Fetch the template
                2. Delete all the components that are attached to the template
                3. Delete the template
                4. Add the new template
            Its a bit convoluted solution but updating it does not seem to work with the current structure of the project.
             */
            var templates = _context.Templates.Where(t => t.TemplateId == request.template.TemplateId).Include(x => x.Components);

            if(templates != null)
            {
                var template = templates.First();
                foreach(Component comp in template.Components)
                {
                    var component = _context.Components.First(x => x.ComponentId == comp.ComponentId);
                    _context.Components.Remove(component);
                }

                _context.Templates.Remove(template);

                await _context.SaveChangesAsync();

                _context.Templates.Add(request.template);
                var res = await _context.SaveChangesAsync();
                return res.ToString();
            }

            throw new Exception("The template looked for was not found");
        }
    }
}
