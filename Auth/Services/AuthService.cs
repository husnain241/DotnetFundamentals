using Auth.DTOs;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IJwtTokenService _jwtTokenService;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
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

            // Load Roles & Claims from Issue #64
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            // Generate Access & Refresh Tokens
            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

            // TODO: Save refreshToken in Database (In Phase 4/Refresh Endpoint step)

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
    }
}