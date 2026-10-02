using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ManagerOrAdmin")]
    public class ManagementController : ControllerBase
    {
        // GET /api/management/reports
        // Accessible by users with "Admin" OR "Manager" role
        [HttpGet("reports")]
        public IActionResult GetReports()
        {
            return Ok(new
            {
                Message = "Here are the management reports.",
                Policy = "ManagerOrAdmin",
                AccessedBy = User.Identity?.Name,
                Roles = User.Claims
                    .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                    .Select(c => c.Value),
                Timestamp = DateTime.UtcNow
            });
        }

        // GET /api/management/team-overview
        // Override class-level policy: only Managers (not Admins)
        [Authorize(Policy = "ManagerOnly")]
        [HttpGet("team-overview")]
        public IActionResult GetTeamOverview()
        {
            return Ok(new
            {
                Message = "Team overview — Manager exclusive view.",
                Policy = "ManagerOnly",
                AccessedBy = User.Identity?.Name,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}