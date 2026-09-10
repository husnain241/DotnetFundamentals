using Auth.DTOs;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
    }
}