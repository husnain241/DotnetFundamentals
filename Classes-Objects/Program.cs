Console.WriteLine("Library Classes");

// Using Default Constructor
Student student1 = new();

// Using Parameterized Constructor
Student student2 = new (001, "Ali Husnain", "Comsats Wah");
Student student3 = new (002, "Hasseb ul Hassan", "UMT Lahore");

Console.WriteLine("\nStudent 1");
Console.WriteLine("------------------");
Console.WriteLine($"Registration Id: {student1.RegId}");
Console.WriteLine($"Full Name: {student1.FullName}");
Console.WriteLine($"University: {student1.University}");

Console.WriteLine("\nStudent 2");
Console.WriteLine("------------------");
Console.WriteLine($"Registration Id: {student2.RegId}");
Console.WriteLine($"Full Namex`: {student2.FullName}");
Console.WriteLine($"University: {student2.University}");

Console.WriteLine("\nStudent 3");
Console.WriteLine("------------------");
Console.WriteLine($"Registration Id: {student3.RegId}");
Console.WriteLine($"Full Name: {student3.FullName}");
Console.WriteLine($"University: {student3.University}");

Student student4 = new()
{
    RegId = 003,
    FullName = "Hafiz Hassan",
    University = "GIKI"
};

Console.WriteLine("\nStudent 4");
Console.WriteLine("------------------");
Console.WriteLine($"Registration Id: {student4.RegId}");
Console.WriteLine($"Full Name: {student4.FullName}");
Console.WriteLine($"University: {student4.University}");


Student student5 = new()
{
    RegId = 005
};

Console.WriteLine("\nStudent 5");
Console.WriteLine("------------------");
Console.WriteLine($"Registration Id: {student5.RegId}");
Console.WriteLine($"Full Name: {student5.FullName}");
Console.WriteLine($"University: {student5.University}");


Console.WriteLine("\npress any key to exit...");
Console.ReadKey();