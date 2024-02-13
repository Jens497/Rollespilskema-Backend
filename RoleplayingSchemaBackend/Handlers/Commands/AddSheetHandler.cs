using Microsoft.AspNetCore.Mvc;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers.Interface;
using RoleplayingSchemaBackend.Requests.Commands;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class AddSheetHandler : ICommandHandler<AddSheetCommand, string>
    {
        private readonly RoleplayingDbContext _context;

        public AddSheetHandler(RoleplayingDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AddSheetCommand request, CancellationToken cancellationToken)
        {
            _context.Sheets.Add(request.Sheet);
            var res = await _context.SaveChangesAsync();
            return res.ToString();
        }
    }
}
