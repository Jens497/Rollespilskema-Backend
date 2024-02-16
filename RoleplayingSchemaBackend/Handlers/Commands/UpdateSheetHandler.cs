using Microsoft.AspNetCore.Routing.Template;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;
using System.Data.Entity;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class UpdateSheetHandler : ICommandHandler<UpdateSheetCommand, string>
    {
        private readonly RoleplayingDbContext _context;

        public UpdateSheetHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateSheetCommand request, CancellationToken cancellationToken)
        {
            var sheets = _context.Sheets.Where(t => t.SheetId == request.sheet.SheetId).Include(x => x.Components);

            if (sheets != null)
            {
                var sheet = sheets.First();
                foreach(Component comp in sheet.Components)
                {
                    var component = _context.Components.First(x => x.ComponentId == comp.ComponentId);
                    _context.Components.Remove(component);
                }

                _context.Sheets.Remove(sheet);

                await _context.SaveChangesAsync();

                _context.Sheets.Add(request.sheet);
                var res = await _context.SaveChangesAsync();
                return res.ToString();
            }

            throw new Exception("The sheet looked for was not found");
        }
    }
}
