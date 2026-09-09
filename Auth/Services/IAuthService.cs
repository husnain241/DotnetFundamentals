using Auth.DTOs;
using System.Security.Claims;

namespace Auth.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterDto dto);
        Task<bool> LoginUserAsync(LoginDto dto);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AddClaimToUserAsync(UserClaimDto dto);
        Task<IList<Claim>> GetUserClaimsAsync(string userId);
    }
}