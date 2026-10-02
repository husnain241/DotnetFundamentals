using Auth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(result);
        }

        [HttpGet("benchmark/optimized")]
        public async Task<IActionResult> GetOptimizedProducts()
        {
            var result = await _productQueryService.GetOptimizedProductsAsync();
            return Ok(result);
        }

        [HttpGet("benchmark/indexed-search")]
        public async Task<IActionResult> GetIndexedFilteredProducts([FromQuery] string name = "Laptop", [FromQuery] decimal maxPrice = 1500)
        {
            var result = await _productQueryService.GetIndexedFilteredProductsAsync(name, maxPrice);
            return Ok(result);
        }
    }
}