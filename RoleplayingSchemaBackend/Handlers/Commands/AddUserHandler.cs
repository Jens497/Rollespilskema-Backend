using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleplayingSchemaBackend.Commands;
using RoleplayingSchemaBackend.Data;
using System.Reflection.Metadata.Ecma335;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class AddUserHandler : IRequestHandler<AddUserCommand>
    {
        //private readonly RoleplayingDbContext _context;
        private readonly UserManager<Users> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        //public AddUserHandler(RoleplayingDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        //public AddUserHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        public AddUserHandler(UserManager<Users> userManager)
        {
            //_context = context;
            _userManager = userManager;
            //_roleManager = roleManager;
        }
        public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("We here");
            var usernameExists = await _userManager.FindByNameAsync(request.User.UserName);
            if (usernameExists != null) 
            {
                Console.Write("Wrong");
                //return "Denied";
            }

            Users user = new()
            {
                Email = request.User.Email != "" ? request.User.Email : "",
                Firstname = request.User.FirstName,
                UserName = request.User.UserName,
                SecurityStamp = Guid.NewGuid().ToString() //This is a must, since this needs to change whenever something happens to the user.
            };

            var res = await _userManager.CreateAsync(user, request.User.Password);
            //return res.Succeeded ? "Success - 201Created" : "Failed to create - 500";
            
            //Old code
            //_context.Users.Add(request.User);
            //await _context.SaveChangesAsync();
        }
    }
}
