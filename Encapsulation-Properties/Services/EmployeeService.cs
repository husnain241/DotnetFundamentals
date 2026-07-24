
using Encapsulation_Properties.Model;

namespace Encapsulation_Properties.Services;

public class EmployeeService
{
    private readonly List<Employee> _employees = new();


    public bool AddEmployee(Employee employee)
    {

        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        if (_employees.Any(e => e.EmployeeId == employee.EmployeeId))
        {
            return false;
        }

        _employees.Add(employee);
        return true;
    }

    // get all list 
    public List<Employee> GetAllEmployees()
    {
        return new List<Employee>(_employees);
    }

    public Employee? SearchEmployeeById(int employeeId)
    {
        return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
    }


    public bool UpdateEmployee(int employeeId, Employee updatedEmployee)
    {
        Employee? employee = SearchEmployeeById(employeeId);

        if (employee == null)
        {
            return false;
        }

        employee.Name = updatedEmployee.Name;
        employee.Department = updatedEmployee.Department;
        employee.MonthlySalary = updatedEmployee.MonthlySalary;

        return true;
    }

    public bool UpdateSalary(int employeeId, double updatedSalary)
    {
        var employee = SearchEmployeeById(employeeId);
        if (employee == null)
        {
            return false;
        }
        employee.MonthlySalary = updatedSalary;
        return true;
    }

    public bool DeleteEmployee(int employeeId)
    {
        var employee = SearchEmployeeById(employeeId);
        if (employee == null) return false;

        _employees.Remove(employee);
        return true;
    }
}
