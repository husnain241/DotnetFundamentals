using Auth.DTOs.Products;
using Auth.Models;

namespace Auth.Services.Interfaces
{
    public interface IProductQueryService
    {
        Task<ProductBenchmarkResultDto<List<Product>>> GetBaselineProductsAsync();

        Task<ProductBenchmarkResultDto<List<ProductDto>>> GetOptimizedProductsAsync();

        Task<ProductBenchmarkResultDto<List<ProductDto>>> GetIndexedFilteredProductsAsync(string searchTerm, decimal maxPrice);
    }
}