using System.ComponentModel.DataAnnotations;

namespace RoleplayingSchemaBackend.Data
{
    public class Users
    {
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
