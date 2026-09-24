using Auth.DTOs;
using Auth.Models;

namespace Auth.Services
{
    public interface IProductQueryService
    {
        Task<ProductBenchmarkResultDto<List<Product>>> GetBaselineProductsAsync();
    }
}