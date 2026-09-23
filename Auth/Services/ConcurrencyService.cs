using Auth.Data;
using Auth.DTOs;
using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Services
{
    public class ConcurrencyService : IConcurrencyService
    {
        private readonly ApplicationDbContext _context;

        public ConcurrencyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<(bool Success, string Message)> UpdateProductOptimisticAsync(UpdateProductDto dto)
        {
            try
            {
                var product = await _context.Products.FindAsync(dto.Id);
                if (product == null)
                    return (false, "Product not found.");

                // Map updated values
                product.Name = dto.Name;
                product.Price = dto.Price;
                product.StockQuantity = dto.StockQuantity;

                // Set original RowVersion to enable EF Core Optimistic Concurrency Check
                _context.Entry(product).Property(p => p.RowVersion).OriginalValue = dto.RowVersion;

                // Save changes
                await _context.SaveChangesAsync();
                return (true, "Product updated successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Conflict caught! Data was modified by another request.
                return (false, "Concurrency Conflict: The record was modified by another user. Please reload and try again.");
            }
        }
    }
}