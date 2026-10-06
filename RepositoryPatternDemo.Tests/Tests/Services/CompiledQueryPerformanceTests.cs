using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories;
using Xunit;

namespace RepositoryPatternDemo.Tests
{
    public class CompiledQueryPerformanceTests
    {
        [Fact]
        public async Task CompiledQuery_ShouldBe_FasterOrEqual_ThanRegularQuery()
        {
            // 1. Arrange: Setup In-Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PerfTestDb_" + Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            // Seed some dummy data (e.g., 1000 students)
            for (int i = 1; i <= 1000; i++)
            {
                context.Students.Add(new Student
                {
                    Id = i,
                    Name = $"Student {i}",
                    DepartmentId = (i % 5) + 1
                });
            }
            await context.SaveChangesAsync();

            var repository = new StudentRepository(context);
            int iterations = 5000; // High iterations to make the time difference measurable
            var stopwatch = new Stopwatch();

            // 2. Act 1: Measure Regular Query Performance
            stopwatch.Start();
            for (int i = 1; i <= iterations; i++)
            {
                // Using the regular sync/async query (simulating normal usage)
                var student = await context.Students
                    .Include(s => s.Department)
                    .FirstOrDefaultAsync(s => s.Id == (i % 1000) + 1);
            }
            stopwatch.Stop();
            long regularQueryTimeMs = stopwatch.ElapsedMilliseconds;

            // 3. Act 2: Measure Compiled Query Performance
            stopwatch.Restart();
            for (int i = 1; i <= iterations; i++)
            {
                // Using our new compiled query method
                var student = await repository.GetByIdCompiledAsync((i % 1000) + 1);
            }
            stopwatch.Stop();
            long compiledQueryTimeMs = stopwatch.ElapsedMilliseconds;

            // 4. Output results to Test Explorer console
            Console.WriteLine($"Regular Query Time:   {regularQueryTimeMs} ms");
            Console.WriteLine($"Compiled Query Time:  {compiledQueryTimeMs} ms");
            Console.WriteLine($"Difference:           {regularQueryTimeMs - compiledQueryTimeMs} ms");

            // 5. Assert: Compiled query should be faster or at least not significantly slower
            // (We add a small buffer of 50ms because In-Memory DB overhead can be noisy)
            Assert.True(compiledQueryTimeMs <= regularQueryTimeMs + 50,
                $"Compiled query ({compiledQueryTimeMs}ms) should be faster than or close to regular query ({regularQueryTimeMs}ms)");
        }
    }
}