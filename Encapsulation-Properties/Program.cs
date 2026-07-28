using Encapsulation_Properties.Model;
using Encapsulation_Properties.Services;

            var employeeService = new EmployeeService();
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": AddEmployee(employeeService); break;
                    case "2": ViewEmployees(employeeService); break;
                    case "3": UpdateSalary(employeeService); break;
                    case "4": DeleteEmployee(employeeService); break;
                    case "5": running = false; break;
                    default: Console.WriteLine("Invalid choice, try again.\n"); break;
                }
            }
        

        static void ShowMenu()
        {
            Console.WriteLine("=== Employee Management System ===");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. Update Salary");
            Console.WriteLine("4. Delete Employee");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
        }

        static void AddEmployee(EmployeeService service)
        {
            try
            {
                Console.Write("Enter Employee ID: ");
                int id = ReadInt();

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Department: ");
                string department = Console.ReadLine();

                Console.Write("Enter Monthly Salary: ");
                decimal salary = ReadDecimal();

                var employee = new Employee(id, name, department, salary);
                bool result = service.AddEmployee(employee);
                Console.WriteLine(result ? "Employee added successfully!\n" : "\n===Employee id already exist, Please try again with new id!===");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        static void ViewEmployees(EmployeeService service)
        {
            var employees = service.GetAllEmployees();

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.\n");
                return;
            }

            foreach (var emp in employees)
                Console.WriteLine(emp);

            Console.WriteLine();
        }

        static void UpdateSalary(EmployeeService service)
        {
            Console.Write("Enter Employee ID to update: ");
            int id = ReadInt();

            Console.Write("Enter new Monthly Salary: ");
            double salary = ReadDouble();

            try
            {
                bool updated = service.UpdateSalary(id, salary);
                Console.WriteLine(updated ? "Salary updated successfully!\n" : "Employee not found.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        static void DeleteEmployee(EmployeeService service)
        {
            Console.Write("Enter Employee ID to delete: ");
            int id = ReadInt();

            bool deleted = service.DeleteEmployee(id);
            Console.WriteLine(deleted ? "Employee deleted successfully!\n" : "Employee not found.\n");
        }

        static int ReadInt()
        {
    int result;
            while (!int.TryParse(Console.ReadLine(), out result))
    {
        Console.Write("Invalid number, try again: ");
    }
    return result;
        }

        static decimal ReadDecimal()
        {
    decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out result))
    {
        Console.Write("Invalid number, try again: ");

    }
    return result;
        }
 