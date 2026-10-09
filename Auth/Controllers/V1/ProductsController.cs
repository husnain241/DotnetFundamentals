using Asp.Versioning;
using Auth.DTOs.Products;
using Auth.Models;
using Auth.Models.Dtos;
using Auth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        private readonly IProductService _productService;

        public ProductsController(IProductQueryService productQueryService, IProductService productService)
        {
            _productQueryService = productQueryService;
            _productService = productService;
        }
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            // Service ko call karein
            var product = await _productService.CreateProductAsync(dto);

            // Response return karein
            return Ok(new
            {
                message = "Product created successfully",
                productId = product.Id
            });
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }

        [HttpPut("products/optimistic")]
        public async Task<IActionResult> UpdateOptimistic([FromBody] UpdateProductDto dto)
        {
            var (success, message) = await _productService.UpdateProductOptimisticAsync(dto);
            if (!success)
            {
                // Return 409 Conflict on concurrency error
                return Conflict(new { message });
            }

            return Ok(new { message });
        }

        [HttpPost("products/pessimistic/{id}/deduct")]
        public async Task<IActionResult> UpdatePessimistic(int id, [FromQuery] int quantity)
        {
            var (success, message) = await _productService.UpdateProductPessimisticAsync(id, quantity);
            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }
    }
}