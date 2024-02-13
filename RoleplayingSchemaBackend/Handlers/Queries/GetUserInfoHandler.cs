using Microsoft.AspNetCore.Identity;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Requests.Interface;
using RoleplayingSchemaBackend.Requests.Queries;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RoleplayingSchemaBackend.Handlers.Queries
{
    public class GetUserInfoHandler : IQueryHandler<GetUserInfoQuery, UserResponseDTO>
    {
        private IHttpContextAccessor _context;
        private SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public GetUserInfoHandler(SignInManager<Users> signInManager, UserManager<Users> userManager, IHttpContextAccessor contextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = contextAccessor;
        }

        public async Task<UserResponseDTO> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userC = _context.HttpContext.User;
            Console.WriteLine(userC.Claims.ToList());

            Users user = _userManager.Users.First(user => user.UserName == userC.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);

            return new UserResponseDTO()
            {
                Email = user.Email,
                FirstName = user.Firstname,
                UserName = user.UserName,
                Roles = roles
            };
        }
    }
}
