using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RoleplayingSchemaBackend.Data
{
    public class Users : IdentityUser
    {
        public string Firstname { get; set; }
    }
}
