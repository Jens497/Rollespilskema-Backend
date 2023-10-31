namespace RoleplayingSchemaBackend
{
    public class UserData
    {
        public static List<User> _users;

        public UserData()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "Test", Email = "Test@gmail.com", Password = "123456" },
                new User { Id = 2, Name = "User2", Email = "User2@gmail.com", Password = "654321" }
            };
        }

        public async Task AddUser(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAllUsers() => await Task.FromResult(_users);
    }
}
