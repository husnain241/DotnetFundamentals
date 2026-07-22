Console.WriteLine("Student Information");
List<Student> students = new List<Student>();
    
while (true)
{
    Console.Clear();

    Console.WriteLine("===== Student Record System =====");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. View Students");
    Console.WriteLine("3. Search Student");
    Console.WriteLine("4. Update Student");
    Console.WriteLine("5. Delete Student");
    Console.WriteLine("6. Exit");

    Console.Write("\nChoose an option: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1": AddStudent(students); break;

        case "2": ViewStudents(students); break;

        case "3": SearchStudent(students); break;

        case "4": UpdateStudent(students); break;

        case "5": DeleteStudent(students); break;

        case "6": return;

        default:
            Console.WriteLine("Invalid choice.");
            Pause();
            break;
    }
}

////methods


static void ViewStudents(List<Student> students)
{
    if (students.Count == 0)
    {
        Console.WriteLine("There is no student on this list.");
    }
    foreach (Student student in students)
    {
        Console.WriteLine();

        Console.WriteLine("Student Id :" + student.Id);
        Console.WriteLine("Student Name :" + student.Name);
        Console.WriteLine("Student Age :" + student.Age);
        Console.WriteLine("Student Grade :" + student.Grade);

    }

    Pause();


}

static void SearchStudent(List<Student> students)
{
    Console.Clear();
    Console.WriteLine("===== Search Student =====");

    Console.Write("Enter the Student ID : ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Please enter correct id");
        Pause();
        return;
    }
    foreach(Student student in students)
    {
        if (id == student.Id)
        {
            Console.WriteLine("Student Id :" + student.Id);
            Console.WriteLine("Student Name :" + student.Name);
            Console.WriteLine("Student Age :" + student.Age);
            Console.WriteLine("Student Grade :" + student.Grade);

            Pause();
            return;
        }
        
    }
    Console.WriteLine("\nStudent not found :");
    Pause();



}

static void UpdateStudent(List<Student> students)
{
    Console.Clear();
    Console.WriteLine("===== Update Student =====");

    Console.Write("Enter the Student ID : ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Please enter correct id");
        Pause();
        return;
    }

    foreach(Student student in students)
    {
        if (id == student.Id)
        {
            Console.Write("Enter ID: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                continue;
            }
            student.Id = id;

            Console.Write("Enter Name: ");
            student.Name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                Console.WriteLine("Name cannot be empty.");
                continue;
            }

            Console.Write("Enter Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
            {
                Console.WriteLine("Invalid Age.");
                continue;
            }
            student.Age = age;

            Console.Write("Enter Grade: ");
            if (!char.TryParse(Console.ReadLine(), out char grade) || !char.IsLetter(grade))
            {
                Console.WriteLine("Invalid Grade.");
                continue;
            }
            student.Grade = grade;

            Console.WriteLine("\nStudent updated successfully.");

            Pause();
            break;

        }
    }

}

static void DeleteStudent(List<Student> students)
{
    Console.Clear();
    Console.WriteLine("===== Search Student =====");

    Console.Write("Enter the Student ID : ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Please enter correct id");
        Pause();
        return;
    }

    students.RemoveAll(s => s.Id == id);
    Console.Write("Delete this Id "+id);

    Pause();



}

static void Pause()
{
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

//add methods to add, view, search, update, and delete students from the list.
static void AddStudent(List<Student> students)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("===== Add Student =====");

        Student student = new Student();

        Console.Write("Enter ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            continue;
        }
        student.Id = id;

        Console.Write("Enter Name: ");
        student.Name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(student.Name))
        {
            Console.WriteLine("Name cannot be empty.");
            continue;
        }

        Console.Write("Enter Age: ");
        if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
        {
            Console.WriteLine("Invalid Age.");
            continue;
        }
        student.Age = age;

        Console.Write("Enter Grade: ");
        if (!char.TryParse(Console.ReadLine(), out char grade) || !char.IsLetter(grade))
        {
            Console.WriteLine("Invalid Grade.");
            continue;
        }
        student.Grade = grade;

        students.Add(student);

        Console.WriteLine("\nStudent added successfully.");

        Pause();
        break;
    }
}
