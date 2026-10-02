using Auth.DTOs;
using Auth.Models;

namespace Auth.Services
{
    public interface IProductQueryService
    {
        Task<ProductBenchmarkResultDto<List<Product>>> GetBaselineProductsAsync();

        Task<ProductBenchmarkResultDto<List<ProductDto>>> GetOptimizedProductsAsync();

        Task<ProductBenchmarkResultDto<List<ProductDto>>> GetIndexedFilteredProductsAsync(string searchTerm, decimal maxPrice);
    }
}