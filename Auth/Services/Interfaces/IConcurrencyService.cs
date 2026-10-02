using Auth.DTOs.Products;
using Auth.Models;

namespace Auth.Services.Interfaces
{
    public interface IConcurrencyService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<(bool Success, string Message)> UpdateProductOptimisticAsync(UpdateProductDto dto);

        Task<(bool Success, string Message)> UpdateProductPessimisticAsync(int productId, int quantityToDeduct);
    }
}