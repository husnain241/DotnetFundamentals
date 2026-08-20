namespace MiniProject_StudentManagementSys.Enums;

/// <summary>
/// Every menu choice, named — no magic numbers scattered through Program.cs.
/// </summary>
public enum MenuOption
{
    AddStudent = 1,
    ViewStudents = 2,
    SearchById = 3,
    UpdateStudent = 4,
    DeleteStudent = 5,
    SearchByName = 6,
    TopStudents = 7,
    StudentsByDepartment = 8,
    StudentsAboveMarks = 9,
    GroupByDepartment = 10,
    ShowStatistics = 11,
    Exit = 0
}
