using Auth.Data;
using Auth.DTOs.Products;
using Auth.Models.Dtos;
using Auth.Models.Product;
using Auth.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auth.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            foreach (var categoryId in dto.CategoryIds)
            {
                var category = new Category { Id = categoryId };

                _context.Categories.Attach(category);

                product.Categories.Add(category);
            }

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

        public async Task<(bool Success, string Message)> UpdateProductPessimisticAsync(int productId, int quantityToDeduct)
        {
            // Begin explicit Database Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Execute SELECT with UPDLOCK and ROWLOCK hints to acquire an exclusive lock
                var product = await _context.Products
                    .FromSqlInterpolated($"SELECT * FROM Products WITH (UPDLOCK, ROWLOCK) WHERE Id = {productId}")
                    .SingleOrDefaultAsync();

                if (product == null)
                {
                    await transaction.RollbackAsync();
                    return (false, "Product not found.");
                }

                // Check business logic condition while row is locked
                if (product.StockQuantity < quantityToDeduct)
                {
                    await transaction.RollbackAsync();
                    return (false, $"Insufficient stock. Current stock is {product.StockQuantity}.");
                }

                // Deduct stock
                product.StockQuantity -= quantityToDeduct;

                // Save changes and Commit transaction (Lock is released after commit)
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, $"Stock updated successfully. Remaining stock: {product.StockQuantity}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Pessimistic Lock Error: {ex.Message}");
            }
        }

        public async Task<PhysicalProduct> CreatePhysicalProductAsync(CreatePhysicalProductDto dto)
        {
            var product = new PhysicalProduct
            {
                Name = dto.Name,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                Weight = dto.Weight,
                Dimensions = dto.Dimensions
            };

            _context.PhysicalProducts.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<DigitalProduct> CreateDigitalProductAsync(CreateDigitalProductDto dto)
        {
            var product = new DigitalProduct
            {
                Name = dto.Name,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                FileSize = dto.FileSize,
                DownloadUrl = dto.DownloadUrl
            };

            _context.DigitalProducts.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}