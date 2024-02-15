using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    public class GetSheetsIdNameHandler : IQueryHandler<GetSheetsIdNameQuery, List<SheetDTO>>
    {
        private readonly RoleplayingDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<Users> _userManager;

        public GetSheetsIdNameHandler(RoleplayingDbContext context, IHttpContextAccessor contextAccessor, UserManager<Users> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<List<SheetDTO>> Handle(GetSheetsIdNameQuery request, CancellationToken cancellationToken)
        {
            var user = _contextAccessor.HttpContext.User;
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }
            var isAdmin = _contextAccessor.HttpContext.User.IsInRole("Admin");
            var userId = _userManager.GetUserId(user);
            if (isAdmin)
            {
                return await _context.Sheets.Select(x => new SheetDTO
                {
                    SheetId = x.SheetId.ToString(),
                    Name = x.Name,
                    TemplateId = x.TemplateId.ToString(),
                    Owner = new SheetUserDTO
                    {
                        FirstName = x.User.Firstname,
                        UserName = x.User.UserName
                    }
                }).ToListAsync();
            } else
            {
                return await _context.Sheets.Where(x => x.User.Id == userId).Select(x => new SheetDTO
                {
                    SheetId = x.SheetId.ToString(),
                    Name = x.Name,
                    TemplateId = x.TemplateId.ToString(),
                    Owner = new SheetUserDTO
                    {
                        FirstName = x.User.Firstname,
                        UserName = x.User.UserName
                    }
                }).ToListAsync();
            }
        }
    }
}
