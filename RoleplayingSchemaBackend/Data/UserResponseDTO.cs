namespace RoleplayingSchemaBackend.Data
{
    public class UserResponseDTO
    {
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string FirstName { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
