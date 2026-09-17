using Auth.DTOs;
using System.Security.Claims;

namespace Auth.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginUserAsync(LoginDto dto);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleToUserAsync(string userId, string roleName);
        Task<bool> AddClaimToUserAsync(UserClaimDto dto);
        Task<IList<Claim>> GetUserClaimsAsync(string userId);

        Task<AuthResponseDto> ExternalLoginCallbackAsync();
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        //Task<bool> RevokeTokenAsync(string refreshToken); 


        Task<bool> ToggleMfaAsync(string userId, bool enable);
        Task<MfaStatusDto> GetMfaStatusAsync(string userId);

        Task<string> GenerateMfaTokenAsync(string userId, string provider);
        Task<bool> VerifyMfaTokenAsync(string userId, string provider, string code);

        Task<AuthResponseDto> VerifyMfaAndGenerateTokensAsync(VerifyMfaDto dto);
    }
}