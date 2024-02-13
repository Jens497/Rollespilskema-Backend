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
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddUserHandler(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<String> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //No reason to check if user already exists, this is done when creating the user.
            //Furhtermore the IdentityModelExcepiton takes care of ALL IdentityModel exceptions.
            if (await _roleManager.RoleExistsAsync(request.User.Role))
            {
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
                            Values = Description.First()
                        }).ToDictionary(d => d.Key, v => v.Values);
                    throw new IdentityModelExceptions(errrosDict);
                }

                await _userManager.AddToRoleAsync(user, request.User.Role);
                Console.WriteLine();
                Console.WriteLine(res1);
                return res1;
            }
            else
            {
                var errsDict = new Dictionary<string, string>
                {
                    {"RoleNotFound", "The role does not exist"}
                };
                throw new IdentityModelExceptions(errsDict);
            }
        }
    }
}