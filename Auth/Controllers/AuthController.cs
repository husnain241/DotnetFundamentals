using Auth.DTOs;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, SignInManager<ApplicationUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }

        // 1. User Register Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(dto);
            if (!result)
                return BadRequest(new { Message = "Registration failed. User might already exist." });

            return Ok(new { Message = "User registered successfully!" });
        }

        // 2. User Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginUserAsync(dto);
            if (!result.IsSuccess)
                return Unauthorized(new { Message = result.Message });

            return Ok(result);
        }

        // 3. Create Role Endpoint (RoleManager Test)
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.CreateRoleAsync(dto.RoleName);
            if (!result)
                return BadRequest(new { Message = "Role creation failed or role already exists." });

            return Ok(new { Message = $"Role '{dto.RoleName}' creat ed successfully!" });
        }

        // 4. Add Custom Claim to User Endpoint (Claims Test)
        [HttpPost("add-claim")]
        public async Task<IActionResult> AddClaim([FromBody] UserClaimDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddClaimToUserAsync(dto);
            if (!result)
                return BadRequest(new { Message = "Failed to add claim. Invalid UserId or Claim format." });

            return Ok(new { Message = "Claim added to user successfully!" });
        }

        // 5. Get User Claims Endpoint
        [HttpGet("user-claims/{userId}")]
        public async Task<IActionResult> GetUserClaims(string userId)
        {
            var claims = await _authService.GetUserClaimsAsync(userId);

            var claimsResponse = claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });

            return Ok(claimsResponse);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RefreshTokenAsync(dto.RefreshToken);
            if (!result.IsSuccess)
                return Unauthorized(new { result.Message });

            return Ok(result);
        }

        // NEW: Protected Endpoint for Testing JWT Authentication
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name;
            var claims = User.Claims.Select(c => new { c.Type, c.Value });

            return Ok(new
            {
                Message = "You have accessed a protected endpoint!",
                UserId = userId,
                Username = username,
                Claims = claims
            });
        }
        // 1. Trigger redirect to Google Login page
        [HttpGet("external-login")]
        public IActionResult ExternalLogin([FromQuery] string provider = "Google")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        // 2. Callback target configured in Google Console (signin-google redirects here internally)
        [HttpGet("external-callback")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var response = await _authService.ExternalLoginCallbackAsync();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}