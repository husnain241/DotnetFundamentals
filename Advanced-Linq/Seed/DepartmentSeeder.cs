using Advanced_Linq.Models;

namespace Advanced_Linq.Seed
{
    public static class DepartmentSeeder
    {
        public static List<Department> GetDepartments()
        {
            return new List<Department>
            {
                new Department { Id = 1, Name = "Computer Science" },
                new Department { Id = 2, Name = "Information Technology" },
                new Department { Id = 3, Name = "Software Engineering" },
                new Department { Id = 4, Name = "Data Science" },
                new Department { Id = 5, Name = "Cyber Security" },
                new Department { Id = 6, Name = "Artificial Intelligence" }
            };
        }
    }
}