using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories;
using RepositoryPatternDemo.Services;
using Xunit;

namespace RepositoryPatternDemo.Tests
{
    public class BulkOperationTests
    {
        // Helper method to create a fresh In-Memory Database for each test
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BulkOpsTest_" + System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void BulkInsert_ShouldAddMultipleStudents()
        {
            // Arrange
            using var context = CreateContext();
            var unitOfWork = new UnitOfWork(context);
            var service = new StudentService(unitOfWork);

            var students = new List<Student>
            {
                new Student { Name = "BulkStudent1", DepartmentId = 1 },
                new Student { Name = "BulkStudent2", DepartmentId = 1 },
                new Student { Name = "BulkStudent3", DepartmentId = 1 }
            };

            // Act
            service.BulkInsertStudents(students);

            // Assert
            Assert.Equal(3, context.Students.Count());
        }

        [Fact]
        public void BulkUpdate_ShouldUpdateRecordsAndReturnAffectedCount()
        {
            // Arrange
            using var context = CreateContext();
            context.Students.AddRange(
                new Student { Name = "S1", DepartmentId = 1 },
                new Student { Name = "S2", DepartmentId = 1 }
            );
            context.SaveChanges();

            var unitOfWork = new UnitOfWork(context);
            var service = new StudentService(unitOfWork);

            // Act (Department 1 ko Department 2 se update karein)
            int affectedRows = service.BulkUpdateDepartmentId(1, 2);

            // Assert
            Assert.Equal(2, affectedRows); // 2 rows update honi chahiye
            Assert.Equal(2, context.Students.Count(s => s.DepartmentId == 2));
        }

        [Fact]
        public void BulkDelete_ShouldDeleteRecordsAndReturnDeletedCount()
        {
            // Arrange
            using var context = CreateContext();
            context.Students.AddRange(
                new Student { Name = "S1", DepartmentId = 1 },
                new Student { Name = "S2", DepartmentId = 1 }
            );
            context.SaveChanges();

            var unitOfWork = new UnitOfWork(context);
            var service = new StudentService(unitOfWork);

            // Act (Department 1 ke saare students delete karein)
            int deletedRows = service.BulkDeleteByDepartmentId(1);

            // Assert
            Assert.Equal(2, deletedRows); // 2 rows delete honi chahiye
            Assert.Empty(context.Students); // Database khali hona chahiye
        }
    }
}