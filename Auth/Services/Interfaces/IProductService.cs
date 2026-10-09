using Auth.DTOs.Products;
using Auth.Models;
using Auth.Models.Dtos;

namespace Auth.Services.Interfaces
{
    public interface IProductService
    {

        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(CreateProductDto dto);
        Task<(bool Success, string Message)> UpdateProductOptimisticAsync(UpdateProductDto dto);
        Task<(bool Success, string Message)> UpdateProductPessimisticAsync(int productId, int quantityToDeduct);
    }
}