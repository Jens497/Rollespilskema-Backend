using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class AddTemplateHandler : ICommandHandler<AddTemplateCommand, String>
    {
        private readonly RoleplayingDbContext _context;

        public AddTemplateHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
        {
            _context.Templates.Add(request.template);
            var res = await _context.SaveChangesAsync();
            return res.ToString();
        }
    }
}