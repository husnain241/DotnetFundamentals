using Auth.Models;
using System.Security.Claims;

namespace Auth.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<Claim> claims);
        RefreshToken GenerateRefreshToken(string userId);
    }
}