using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RoleplayingSchemaBackend.Data;

namespace RoleplayingSchemaBackend.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<Users> _signInManager;

        public AuthController(SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
        }


    }
}
