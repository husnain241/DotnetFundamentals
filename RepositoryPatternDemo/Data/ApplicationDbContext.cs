using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Computer Science" },
                new Department { Id = 2, Name = "Software Engineering" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Ali", Email = "ali@example.com", DepartmentId = 1 },
                new Student { Id = 2, Name = "Ahmed", Email = "ahmed@example.com", DepartmentId = 2 }
            );
        }
    }
}
