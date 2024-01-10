using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RoleplayingSchemaBackend.Data
{
    public class Users : IdentityUser
    {
        [Required]
        public string Firstname { get; set; }
    }
}
