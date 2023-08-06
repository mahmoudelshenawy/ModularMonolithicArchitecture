using Microsoft.AspNetCore.Identity;
using Module.Users.Core.Entities;
using System.Security.Claims;

namespace Module.Users.Core.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        Task<(string, DateTime)> CreateJwtToken(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
