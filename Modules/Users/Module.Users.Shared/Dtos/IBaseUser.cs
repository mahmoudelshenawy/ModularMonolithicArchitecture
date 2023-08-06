namespace Module.Users.Shared.Dtos
{
    public class IBaseUser
    {
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public List<string> Roles { get; private set; }
        public Dictionary<string, List<object>> Relations { get; set; } = new();

        public IBaseUser(string userId, string email, string username, List<string> roles,
            Dictionary<string, List<object>> relations = null)
        {
            UserId = userId;
            Email = email;
            Username = username;
            Roles = roles;
            Relations = relations;
        }
    }
}
