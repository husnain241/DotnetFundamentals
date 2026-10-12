using Auth.DTOs.Tenant;
using Auth.Services;
using Auth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // POST: api/tenants
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantDto dto)
        {
            try
            {
                var result = await _tenantService.CreateTenantAsync(dto);
                return CreatedAtAction(nameof(GetAllTenants), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                // Validation error (400 Bad Request)
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Duplication error (409 Conflict)
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }

        // GET: api/tenants
        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();
            return Ok(tenants);
        }
    }
}