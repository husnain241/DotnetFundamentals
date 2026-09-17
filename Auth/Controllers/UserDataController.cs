using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "UserAccess")]
    public class UserDataController : ControllerBase
    {
        // GET /api/userdata/my-data
        // Accessible by users with the "User" role
        [HttpGet("my-data")]
        public IActionResult GetMyData()
        {
            return Ok(new
            {
                Message = "Here is your personal data.",
                Policy = "UserAccess",
                AccessedBy = User.Identity?.Name,
                UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}