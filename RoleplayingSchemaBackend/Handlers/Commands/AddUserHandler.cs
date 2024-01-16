using MediatR;
using Microsoft.AspNetCore.Identity;
using RoleplayingSchemaBackend.Commands;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Exceptions;
using RoleplayingSchemaBackend.Handlers.Interface;
using System.Reflection.Metadata.Ecma335;

namespace RoleplayingSchemaBackend.Handlers.Commands
{
    public class AddUserHandler : ICommandHandler<AddUserCommand, String>
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
        public async Task<String> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("We here");
            var usernameExists = await _userManager.FindByNameAsync(request.User.UserName);
            if (usernameExists != null) 
            {
                Console.Write("Wrong");
                //return "Denied";
                return "Errorcode: 400, The user already exists!";
            }

            Users user = new()
            {
                Email = request.User.Email != "" ? request.User.Email : "",
                Firstname = request.User.FirstName,
                UserName = request.User.UserName,
                SecurityStamp = Guid.NewGuid().ToString() //This is a must, since this needs to change whenever something happens to the user.
            };
            var res = await _userManager.CreateAsync(user, request.User.Password);
            res.Errors.ToList();
            var res1 = res.ToString();
            var errorList = res.Errors.ToList();
            if (errorList.Any())
            {
                var errrosDict = errorList.GroupBy(e => e.Code, e => e.Description,
                    (Code, Description) => new
                    {
                        Key = Code,
                        Values = Description.Distinct().ToArray() //This might need to be array (atleast not for password erros) but its here in case of other erros needing it.
                    }).ToDictionary(d => d.Key, v => v.Values);
                throw new IdentityModelExceptions(errrosDict);
            }

            Console.WriteLine();
            Console.WriteLine(res1);
            return res1;
        }
    }
}
