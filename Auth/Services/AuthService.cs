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

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
        public async Task<bool> LoginUserAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.UserName,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            return result.Succeeded;
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