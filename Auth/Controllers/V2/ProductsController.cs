using Auth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace Auth.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQueryService _productQueryService;

        public ProductsController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        [HttpGet("benchmark/baseline")]
        public async Task<IActionResult> GetBaselineProducts()
        {
            var result = await _productQueryService.GetBaselineProductsAsync();
            return Ok(new { Version = "2.0", Timestamp = DateTime.UtcNow, Data = result });
        }

        [HttpGet("benchmark/optimized")]
        public async Task<IActionResult> GetOptimizedProducts()
        {
            var result = await _productQueryService.GetOptimizedProductsAsync();
            return Ok(new { Version = "2.0", Timestamp = DateTime.UtcNow, Data = result });
        }

        [HttpGet("benchmark/indexed-search")]
        public async Task<IActionResult> GetIndexedFilteredProducts([FromQuery] string name = "Laptop", [FromQuery] decimal maxPrice = 1500)
        {
            var result = await _productQueryService.GetIndexedFilteredProductsAsync(name, maxPrice);
            return Ok(new { Version = "2.0", Timestamp = DateTime.UtcNow, Data = result });
        }
    }
}