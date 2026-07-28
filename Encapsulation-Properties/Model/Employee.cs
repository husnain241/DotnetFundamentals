namespace Encapsulation_Properties.Model;


public class Employee{
    private int _employeeId;
    private string _name;
    private string _department;
    private decimal _monthlySalary;
        
    public int EmployeeId
    {
        get => _employeeId;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Employee ID must be positive.");
            _employeeId = value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");
            _name = value.Trim();
        }
    }

    public string Department
    {
        get => _department;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Department cannot be empty.");
            _department = value.Trim();
        }
    }

    public decimal MonthlySalary
    {
        get => _monthlySalary;
        set
        {
            if (value < 0)
                throw new ArgumentException("Salary cannot be negative.");
            _monthlySalary = value;
        }
    }

    public decimal AnnualSalary => _monthlySalary * 12;


    public string SalaryGrade
    {
        get
        {
            if (_monthlySalary >= 200000) return "Senior";
            if (_monthlySalary >= 100000) return "Mid";
            return "Junior";
        }
    }

    public Employee(int employeeId, string name, string department, decimal monthlySalary)
    {
        EmployeeId = employeeId;
        Name = name;
        Department = department;
        MonthlySalary = monthlySalary;
    }

    public override string ToString()
    {
        return $"ID: {EmployeeId} | Name: {Name} | Dept: {Department} | " +
               $"Monthly: {MonthlySalary:F2} | Annual: {AnnualSalary:F2} | Grade: {SalaryGrade}";
    }
}