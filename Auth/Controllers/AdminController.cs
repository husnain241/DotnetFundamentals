using Auth.DTOs;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET /api/admin/dashboard
        // Only accessible by users with the "Admin" role
        [HttpGet("dashboard")]
        public IActionResult GetDashboard()
        {
            return Ok(new
            {
                Message = "Welcome to the Admin Dashboard!",
                Policy = "AdminOnly",
                AccessedBy = User.Identity?.Name,
                Timestamp = DateTime.UtcNow
            });
        }

        // POST /api/admin/assign-role
        // Allows an Admin to assign a role to any user
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AssignRoleToUserAsync(dto.UserId, dto.RoleName);
            if (!result)
                return BadRequest(new { Message = "Failed to assign role. Check UserId and RoleName." });

            return Ok(new { Message = $"Role '{dto.RoleName}' assigned successfully to user '{dto.UserId}'." });
        }

        [HttpGet("active-user")]
        public async Task<IActionResult> GetActiveUser([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { Message = "Email is required." });

            var user = await _authService.GetActiveUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { Message = "Active user not found or email unconfirmed." });
            }

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.EmailConfirmed,
                user.PhoneNumberConfirmed
            });
        }

        // 2. Soft Deactivate User (Consumes Custom ApplicationUserManager)
        [HttpPost("deactivate-user/{userId}")]
        public async Task<IActionResult> DeactivateUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Message = "UserId is required." });

            var result = await _authService.DeactivateUserAccountAsync(userId);
            if (!result)
            {
                return BadRequest(new { Message = "Failed to deactivate user or user not found." });
            }

            return Ok(new { Message = "User account deactivated/locked successfully!" });
        }

        [HttpPost("custom-role")]
        public async Task<IActionResult> CreateCustomRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest(new { Message = "Role name is required." });

            var result = await _authService.CreateCustomRoleAsync(roleName);
            if (!result.Succeeded)
            {
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }

            return Ok(new { Message = $"Custom role '{roleName}' created successfully!" });
        }

        [HttpDelete("safe-delete-role/{roleName}")]
        public async Task<IActionResult> SafeDeleteRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest(new { Message = "Role name is required." });

            var result = await _authService.SafeDeleteRoleAsync(roleName);
            if (!result.Succeeded)
            {
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }

            return Ok(new { Message = $"Role '{roleName}' was safely deleted." });
        }
    }
}