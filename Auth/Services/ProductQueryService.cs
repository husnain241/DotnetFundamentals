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
    }
}