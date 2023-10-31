using System.ComponentModel.DataAnnotations;

namespace RoleplayingSchemaBackend
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        
    }
}
