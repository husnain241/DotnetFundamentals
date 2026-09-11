using Auth.DTOs;
using System.Security.Claims;

namespace Auth.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginUserAsync(LoginDto dto);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AddClaimToUserAsync(UserClaimDto dto);
        Task<IList<Claim>> GetUserClaimsAsync(string userId);

        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken); 
        //Task<bool> RevokeTokenAsync(string refreshToken); 
    }
}