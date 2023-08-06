using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Users.Core.Dtos
{
    public class UserDetailsDto
    {
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public List<string> Roles { get; private set; }
        public Dictionary<string, List<object>> Relations { get; set; } = new();

        public UserDetailsDto(string userId, string email, string username, List<string> roles)
        {
            UserId = userId;
            Email = email;
            Username = username;
            Roles = roles;
        }
    }
}
