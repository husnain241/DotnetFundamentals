using Auth.Data;
using Auth.DTOs;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context; 

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtTokenService jwtTokenService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        // 1. User Registration Logic
        public async Task<bool> RegisterUserAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return false;

            // Default Claim automatically attach karte hain
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));

            return true;
        }

        // 2. User Login Logic
        public async Task<AuthResponseDto> LoginUserAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid username or password." };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid username or password." };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Login successful!",
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        // 3. Role Creation Logic
        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName)) return false;

            var result = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            return result.Succeeded;
        }

        // 4. Custom Claim Assignment
        public async Task<bool> AddClaimToUserAsync(UserClaimDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return false;

            var claim = new Claim(dto.ClaimType, dto.ClaimValue);
            var result = await _userManager.AddClaimAsync(user, claim);

            return result.Succeeded;
        }

        // 5. Get User Claims
        public async Task<IList<Claim>> GetUserClaimsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<Claim>();

            return await _userManager.GetClaimsAsync(user);
        }
        public async Task<AuthResponseDto> RefreshTokenAsync(string token)
        {
            var storedToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token);

            if (storedToken == null || !storedToken.IsActive)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid or expired refresh token." };
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "User not found." };
            }

            // Step A: REVOKE the old refresh token
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);
            storedToken.RevokedAt = DateTime.UtcNow;
            storedToken.ReplacedByToken = newRefreshToken.Token;

            // Step B: Save NEW refresh token
            await _context.RefreshTokens.AddAsync(newRefreshToken);
            await _context.SaveChangesAsync();

            // Step C: Generate NEW Access Token
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            var newAccessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Tokens refreshed successfully!",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        public async Task<AuthResponseDto> ExternalLoginCallbackAsync()
        {
            // 1. Read external identity details from the temporary cookie scheme
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Error loading external login information." };
            }
                
            // 2. Sign in the user with this external login provider if already linked
            var result = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: false,
                bypassTwoFactor: true);

            ApplicationUser? user = null;

            if (result.Succeeded)
            {
                // User exists and is linked
                user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            }
            else
            {
                // 3. User is signing in with Google for the first time
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        // Create a new local ApplicationUser
                        user = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = email,
                            Email = email,
                            EmailConfirmed = true
                        };

                        var createResult = await _userManager.CreateAsync(user);
                        if (!createResult.Succeeded)
                        {
                            return new AuthResponseDto { IsSuccess = false, Message = "Failed to create local user for external identity." };
                        }
                    }

                    // Link the Google Provider identity to the ApplicationUser (AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Unable to process user identity." };
            }

            // 4. Generate your application's JWT + Refresh Tokens (Issues #63–65 architecture)
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = $"Successfully authenticated via {info.LoginProvider}!",
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}