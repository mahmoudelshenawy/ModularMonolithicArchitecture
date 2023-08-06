using Microsoft.AspNetCore.Identity;

namespace Module.Users.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Status { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}
