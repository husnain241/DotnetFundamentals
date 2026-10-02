using System.Diagnostics;
using Auth.Data;
using Auth.DTOs;
using Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Services
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly ApplicationDbContext _context;

        public ProductQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductBenchmarkResultDto<List<Product>>> GetBaselineProductsAsync()
        {
            // 1. Un-optimized Query (SELECT *, Change Tracking Active)
            var query = _context.Products;

            // 2. Exact SQL Query text capture
            string generatedSql = query.ToQueryString();

            // 3. Execution Time Benchmarking
            var stopwatch = Stopwatch.StartNew();
            var products = await query.ToListAsync();
            stopwatch.Stop();

            return new ProductBenchmarkResultDto<List<Product>>
            {
                GeneratedSql = generatedSql,
                ExecutionTimeMilliseconds = stopwatch.ElapsedMilliseconds,
                RecordCount = products.Count,
                Data = products
            };
        }
        public async Task<ProductBenchmarkResultDto<List<ProductDto>>> GetOptimizedProductsAsync()
        {
            // 1. Optimized EF Core LINQ Query
            // - AsNoTracking(): Change Tracker Overhead Disable
            // - Select(): Selective Column Projection (Only Id, Name, Price)
            var query = _context.Products
                .AsNoTracking()
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });

            // 2. Exact Optimized SQL Query String Capture
            string generatedSql = query.ToQueryString();

            // 3. Execution Time Benchmarking
            var stopwatch = Stopwatch.StartNew();
            var products = await query.ToListAsync();
            stopwatch.Stop();

            return new ProductBenchmarkResultDto<List<ProductDto>>
            {
                GeneratedSql = generatedSql,
                ExecutionTimeMilliseconds = stopwatch.ElapsedMilliseconds,
                RecordCount = products.Count,
                Data = products
            };
        }

        public async Task<ProductBenchmarkResultDto<List<ProductDto>>> GetIndexedFilteredProductsAsync(string searchTerm, decimal maxPrice)
        {
            // High-frequency filtered query utilizing Indexes (IX_Products_Name & IX_Products_Price)
            var query = _context.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(searchTerm) && p.Price <= maxPrice)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });

            string generatedSql = query.ToQueryString();

            var stopwatch = Stopwatch.StartNew();
            var products = await query.ToListAsync();
            stopwatch.Stop();

            return new ProductBenchmarkResultDto<List<ProductDto>>
            {
                GeneratedSql = generatedSql,
                ExecutionTimeMilliseconds = stopwatch.ElapsedMilliseconds,
                RecordCount = products.Count,
                Data = products
            };
        }
    }
}