using System;
using System.Collections.Generic;
using Linq_Basics.Models;

namespace Linq_Basics.Seed
{
    public static class StudentSeeder
    {
        public static List<Student> GetStudents()
        {
            var firstNames = new string[]
            {
                "Ali", "Ahmed", "Sara", "Hamza", "Ayesha", "Usman", "Zain", "Fatima",
                "Hassan", "Maryam", "Bilal", "Sana", "Umar", "Hina", "Faisal", "Zainab",
                "Imran", "Nida", "Waqas", "Iqra", "Kashif", "Rabia", "Adeel", "Mahnoor",
                "Tariq", "Amna", "Salman", "Sidra", "Farhan", "Noor", "Asad", "Laiba",
                "Shahzad", "Hira", "Junaid", "Komal", "Naveed", "Areeba", "Rizwan", "Anum",
                "Danish", "Warda", "Shoaib", "Mehak", "Talha", "Sadia", "Aamir", "Nimra",
                "Yasir", "Sundas"
            };

            var lastNames = new string[]
            {
                "Khan", "Malik", "Butt", "Chaudhry", "Sheikh", "Raza", "Iqbal", "Farooq",
                "Javed", "Aslam", "Hussain", "Abbas", "Qureshi", "Siddiqui", "Baig",
                "Ansari", "Mirza", "Yousaf", "Akhtar", "Rashid"
            };

            var departments = new string[]
            {
                "Computer Science",
                "Information Technology",
                "Software Engineering",
                "Data Science",
                "Cyber Security",
                "Artificial Intelligence"
            };

            var students = new List<Student>();
            var random = new Random(42); // fixed seed => har baar same data milega

            for (int i = 1; i <= 100; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];

                students.Add(new Student
                {
                    Id = i,
                    Name = $"{firstName} {lastName}",
                    Age = random.Next(18, 26),              // 18 se 25 tak
                    Department = departments[random.Next(departments.Length)],
                    Marks = random.Next(40, 100)             // 40 se 99 tak
                });
            }

            return students;
        }
    }
}