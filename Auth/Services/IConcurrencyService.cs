using Auth.DTOs;
using Auth.Models;

namespace Auth.Services
{
    public interface IConcurrencyService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<(bool Success, string Message)> UpdateProductOptimisticAsync(UpdateProductDto dto);
    }
}