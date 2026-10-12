using Microsoft.EntityFrameworkCore;
using UMS_Sp.DTOs;
using UMS_Sp.Models;

namespace UMS_Sp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department Configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.HeadOfDepartment).IsRequired().HasMaxLength(100);
            });

            // Student Configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(s => s.Email).IsUnique();

                // Relationship: Department (1) <-> (Many) Students
                entity.HasOne(s => s.Department)
                      .WithMany(d => d.Students)
                      .HasForeignKey(s => s.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Course Configuration
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(150);

                // Relationship: Department (1) <-> (Many) Courses
                entity.HasOne(c => c.Department)
                      .WithMany(d => d.Courses)
                      .HasForeignKey(c => c.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Enrollment Configuration (Many-to-Many join entity)
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Grade).HasMaxLength(2);
                entity.Property(e => e.Marks).HasPrecision(5, 2);

                // Ensure a student cannot be enrolled in the same course more than once
                entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();

                // Relationship: Student (1) <-> (Many) Enrollments
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Enrollments)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship: Course (1) <-> (Many) Enrollments
                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<StudentDepartmentDto>().HasNoKey();

        }
    }
}
