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
    }
}