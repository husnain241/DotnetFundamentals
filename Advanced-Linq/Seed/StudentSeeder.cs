using Advanced_Linq.Models;

namespace Advanced_Linq.Seed
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

			var students = new List<Student>();
			var random = new Random(42);

			for (int i = 1; i <= 100; i++)
			{
				string firstName = firstNames[random.Next(firstNames.Length)];
				string lastName = lastNames[random.Next(lastNames.Length)];

				students.Add(new Student
				{
					Id = i,
					Name = $"{firstName} {lastName}",
					Age = random.Next(18, 26),
					Marks = random.Next(40, 100),

					// Random Department Id (1 to 6)
					DepartmentId = random.Next(1, 7)
				});
			}

			return students;
		}
	}
}