using Auth.DTOs;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcurrencyController : ControllerBase
    {
        private readonly IConcurrencyService _concurrencyService;

        public ConcurrencyController(IConcurrencyService concurrencyService)
        {
            _concurrencyService = concurrencyService;
        }

        // GET: api/concurrency/products/5
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _concurrencyService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }

        // POST: api/concurrency/products
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var createdProduct = await _concurrencyService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/concurrency/products/optimistic
        [HttpPut("products/optimistic")]
        public async Task<IActionResult> UpdateOptimistic([FromBody] UpdateProductDto dto)
        {
            var (success, message) = await _concurrencyService.UpdateProductOptimisticAsync(dto);
            if (!success)
            {
                // Return 409 Conflict on concurrency error
                return Conflict(new { message });
            }

            return Ok(new { message });
        }

        // POST: api/concurrency/products/pessimistic/5/deduct?quantity=2
        [HttpPost("products/pessimistic/{id}/deduct")]
        public async Task<IActionResult> UpdatePessimistic(int id, [FromQuery] int quantity)
        {
            var (success, message) = await _concurrencyService.UpdateProductPessimisticAsync(id, quantity);
            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }
    }
}