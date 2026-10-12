using Microsoft.EntityFrameworkCore;
using UMS_Sp.Models;

namespace UMS_Sp.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await context.Departments.AnyAsync())
            {
                return; // DB has been seeded
            }

            // Seed Departments
            var csDept = new Department
            {
                Name = "Computer Science",
                HeadOfDepartment = "Dr. Alan Turing",
                CreatedAt = DateTime.UtcNow.AddMonths(-12)
            };
            var eeDept = new Department
            {
                Name = "Electrical Engineering",
                HeadOfDepartment = "Dr. Nikola Tesla",
                CreatedAt = DateTime.UtcNow.AddMonths(-10)
            };
            var bbaDept = new Department
            {
                Name = "Business Administration",
                HeadOfDepartment = "Dr. Peter Drucker",
                CreatedAt = DateTime.UtcNow.AddMonths(-8)
            };

            await context.Departments.AddRangeAsync(csDept, eeDept, bbaDept);
            await context.SaveChangesAsync();

            // Seed Courses
            var course1 = new Course
            {
                Title = "Introduction to Programming",
                Credits = 4,
                DepartmentId = csDept.Id
            };
            var course2 = new Course
            {
                Title = "Data Structures and Algorithms",
                Credits = 3,
                DepartmentId = csDept.Id
            };
            var course3 = new Course
            {
                Title = "Digital Logic Design",
                Credits = 3,
                DepartmentId = eeDept.Id
            };
            var course4 = new Course
            {
                Title = "Principles of Management",
                Credits = 3,
                DepartmentId = bbaDept.Id
            };

            await context.Courses.AddRangeAsync(course1, course2, course3, course4);
            await context.SaveChangesAsync();

            // Seed Students
            var student1 = new Student
            {
                Name = "Alice Johnson",
                Email = "alice.johnson@university.edu",
                EnrollmentDate = DateTime.UtcNow.AddMonths(-6),
                DepartmentId = csDept.Id
            };
            var student2 = new Student
            {
                Name = "Bob Smith",
                Email = "bob.smith@university.edu",
                EnrollmentDate = DateTime.UtcNow.AddMonths(-5),
                DepartmentId = csDept.Id
            };
            var student3 = new Student
            {
                Name = "Charlie Brown",
                Email = "charlie.brown@university.edu",
                EnrollmentDate = DateTime.UtcNow.AddMonths(-4),
                DepartmentId = eeDept.Id
            };
            var student4 = new Student
            {
                Name = "Diana Prince",
                Email = "diana.prince@university.edu",
                EnrollmentDate = DateTime.UtcNow.AddMonths(-3),
                DepartmentId = bbaDept.Id
            };

            await context.Students.AddRangeAsync(student1, student2, student3, student4);
            await context.SaveChangesAsync();

            // Seed Enrollments (Many-to-Many relationships)
            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentId = student1.Id,
                    CourseId = course1.Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-5),
                    Grade = "A",
                    Marks = 92.5m
                },
                new Enrollment
                {
                    StudentId = student1.Id,
                    CourseId = course2.Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-4),
                    Grade = "B",
                    Marks = 84.0m
                },
                new Enrollment
                {
                    StudentId = student2.Id,
                    CourseId = course1.Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-5),
                    Grade = "B",
                    Marks = 81.0m
                },
                new Enrollment
                {
                    StudentId = student3.Id,
                    CourseId = course3.Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-3),
                    Grade = "A",
                    Marks = 95.0m
                },
                new Enrollment
                {
                    StudentId = student4.Id,
                    CourseId = course4.Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Grade = "C",
                    Marks = 74.5m
                }
            };

            await context.Enrollments.AddRangeAsync(enrollments);
            await context.SaveChangesAsync();
        }
    }
}
