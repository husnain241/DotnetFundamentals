using MiniProject_StudentManagementSys.Constants;
using MiniProject_StudentManagementSys.Enums;
using MiniProject_StudentManagementSys.Helpers;
using MiniProject_StudentManagementSys.Models;
using MiniProject_StudentManagementSys.Services;
using MiniProject_StudentManagementSys.Interfaces;

namespace MiniProject_StudentManagementSys.UI;

public class StudentMenu
{
    private readonly IStudentService _studentService;

    public StudentMenu(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            ShowMenu();

            int choice = ConsoleHelper.ReadInt(Messages.MenuPrompt);

            if (!Enum.IsDefined(typeof(MenuOption), choice))
            {
                ConsoleHelper.PrintError(Messages.InvalidMenuChoice);
                ConsoleHelper.Pause();
                continue;
            }

            isRunning = HandleChoice((MenuOption)choice);
        }
    }

    private void ShowMenu()
    {
        Console.Clear();

        Console.WriteLine(Messages.MenuTitle);
        Console.WriteLine("1.  Add Student");
        Console.WriteLine("2.  View All Students");
        Console.WriteLine("3.  Search Student by ID");
        Console.WriteLine("4.  Update Student");
        Console.WriteLine("5.  Delete Student");
        Console.WriteLine("6.  Search Student by Name");
        Console.WriteLine("7.  Show Top Students");
        Console.WriteLine("8.  Show Students by Department");
        Console.WriteLine("9.  Show Students Above Marks");
        Console.WriteLine("10. Group Students by Department");
        Console.WriteLine("11. Show Statistics");
        Console.WriteLine("0.  Exit");
        Console.WriteLine();
    }

    private bool HandleChoice(MenuOption option)
    {
        switch (option)
        {
            case MenuOption.AddStudent:
                AddStudent();
                break;

            case MenuOption.ViewStudents:
                ViewStudents();
                break;

            case MenuOption.SearchById:
                SearchById();
                break;

            case MenuOption.UpdateStudent:
                UpdateStudent();
                break;

            case MenuOption.DeleteStudent:
                DeleteStudent();
                break;

            case MenuOption.SearchByName:
                SearchByName();
                break;

            case MenuOption.TopStudents:
                ShowTopStudents();
                break;

            case MenuOption.StudentsByDepartment:
                ShowStudentsByDepartment();
                break;

            case MenuOption.StudentsAboveMarks:
                ShowStudentsAboveMarks();
                break;

            case MenuOption.GroupByDepartment:
                ShowGroupedByDepartment();
                break;

            case MenuOption.ShowStatistics:
                ShowStatistics();
                break;

            case MenuOption.Exit:
                return false;
        }

        ConsoleHelper.Pause();
        return true;
    }

    private void AddStudent()
    {
        ConsoleHelper.PrintHeader("--- Add Student ---");

        int id = ConsoleHelper.ReadInt(Messages.PromptStudentId);

        if (!ValidationHelper.IsPositiveId(id) || _studentService.IsIdTaken(id))
        {
            ConsoleHelper.PrintError(Messages.DuplicateId);
            return;
        }

        string name = ConsoleHelper.ReadNonEmptyString(Messages.PromptStudentName);

        double marks = ConsoleHelper.ReadDouble(Messages.PromptMarks);

        if (!ValidationHelper.IsMarksInRange(marks))
        {
            ConsoleHelper.PrintError(Messages.InvalidMarksRange);
            return;
        }

        int departmentId = ConsoleHelper.ReadInt(Messages.PromptDepartmentId);

        if (!_studentService.DepartmentExists(departmentId))
        {
            ConsoleHelper.PrintError(Messages.InvalidDepartment);
            return;
        }

        _studentService.AddStudent(new Student(id, name, marks, departmentId));

        ConsoleHelper.PrintSuccess(Messages.StudentAdded);
    }

    private void ViewStudents()
    {
        ConsoleHelper.PrintHeader(Messages.HeaderAllStudents);

        PrintStudentList(
            _studentService
                .GetAllWithDepartmentNames()
                .Select(student => student.ToString()));
    }

    private void SearchById()
    {
        int id = ConsoleHelper.ReadInt(Messages.PromptStudentId);

        var student = _studentService.GetById(id);

        if (student is null)
        {
            ConsoleHelper.PrintError(Messages.StudentNotFound);
            return;
        }

        Console.WriteLine(student);
    }

    private void UpdateStudent()
    {
        ConsoleHelper.PrintHeader("--- Update Student ---");

        int id = ConsoleHelper.ReadInt(Messages.PromptStudentId);

        if (_studentService.GetById(id) is null)
        {
            ConsoleHelper.PrintError(Messages.StudentNotFound);
            return;
        }

        string name = ConsoleHelper.ReadNonEmptyString(Messages.PromptStudentName);

        double marks = ConsoleHelper.ReadDouble(Messages.PromptMarks);

        if (!ValidationHelper.IsMarksInRange(marks))
        {
            ConsoleHelper.PrintError(Messages.InvalidMarksRange);
            return;
        }

        int departmentId = ConsoleHelper.ReadInt(Messages.PromptDepartmentId);

        if (!_studentService.DepartmentExists(departmentId))
        {
            ConsoleHelper.PrintError(Messages.InvalidDepartment);
            return;
        }

        _studentService.UpdateStudent(id, name, marks, departmentId);

        ConsoleHelper.PrintSuccess(Messages.StudentUpdated);
    }

    private void DeleteStudent()
    {
        int id = ConsoleHelper.ReadInt(Messages.PromptStudentId);

        if (!_studentService.DeleteStudent(id))
        {
            ConsoleHelper.PrintError(Messages.StudentNotFound);
            return;
        }

        ConsoleHelper.PrintSuccess(Messages.StudentDeleted);
    }

    private void SearchByName()
    {
        string term = ConsoleHelper.ReadNonEmptyString(Messages.PromptSearchName);

        var results = _studentService.SearchByName(term);

        PrintStudentList(results.Select(student => student.ToString()));
    }

    private void ShowTopStudents()
    {
        int count = ConsoleHelper.ReadInt(Messages.PromptTopN);

        var results = _studentService.GetTopStudents(count);

        PrintStudentList(results.Select(student => student.ToString()));
    }

    private void ShowStudentsByDepartment()
    {
        int departmentId = ConsoleHelper.ReadInt(Messages.PromptDepartmentId);

        if (!_studentService.DepartmentExists(departmentId))
        {
            ConsoleHelper.PrintError(Messages.InvalidDepartment);
            return;
        }

        var results = _studentService.GetStudentsByDepartment(departmentId);

        PrintStudentList(results.Select(student => student.ToString()));
    }

    private void ShowStudentsAboveMarks()
    {
        double minMarks = ConsoleHelper.ReadDouble(Messages.PromptMinMarks);

        var results = _studentService.GetStudentsAboveMarks(minMarks);

        PrintStudentList(results.Select(student => student.ToString()));
    }

    private void ShowGroupedByDepartment()
    {
        ConsoleHelper.PrintHeader(Messages.HeaderGroupByDepartment);

        var grouped = _studentService.GroupByDepartment();

        if (!grouped.Any())
        {
            ConsoleHelper.PrintError(Messages.NoStudentsFound);
            return;
        }

        foreach (var (department, students) in grouped)
        {
            Console.WriteLine($"\n{department}:");

            foreach (var student in students)
            {
                Console.WriteLine($"  {student}");
            }
        }
    }

    private void ShowStatistics()
    {
        ConsoleHelper.PrintHeader(Messages.HeaderStatistics);

        if (_studentService.GetTotalStudents() == 0)
        {
            ConsoleHelper.PrintError(Messages.NoStudentsFound);
            return;
        }

        Console.WriteLine($"Total Students : {_studentService.GetTotalStudents()}");
        Console.WriteLine($"Highest Marks  : {_studentService.GetHighestMarks():F2}");
        Console.WriteLine($"Lowest Marks   : {_studentService.GetLowestMarks():F2}");
        Console.WriteLine($"Average Marks  : {_studentService.GetAverageMarks():F2}");

        Console.WriteLine("\nDepartment-wise Student Count:");

        foreach (var (department, count) in _studentService.GetDepartmentWiseCount())
        {
            Console.WriteLine($"  {department}: {count}");
        }
    }

    private static void PrintStudentList(IEnumerable<string> lines)
    {
        List<string> list = lines.ToList();

        if (!list.Any())
        {
            ConsoleHelper.PrintError(Messages.NoStudentsFound);
            return;
        }

        foreach (string line in list)
        {
            Console.WriteLine(line);
        }
    }
}