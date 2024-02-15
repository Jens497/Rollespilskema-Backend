using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            Sheet sheet = new Sheet(){
                Components = request.sheetDto.Components,
                Name = request.sheetDto.Name,
                SheetId = request.sheetDto.SheetId,
                TemplateId = request.sheetDto.TemplateId,
                UserId = request.sheetDto.UserId
            };
            _context.Sheets.Add(sheet);
            //_context.Users.FirstAsync(x => x.Id == request.sheetDto.UserId);
            var res = await _context.SaveChangesAsync();
            return res.ToString();
        }
    }
}
