namespace MiniProject_StudentManagementSys.Constants;

/// <summary>
/// Every user-facing string lives here. Avoids magic strings and makes
/// wording changes (or future localization) a one-file job.
/// </summary>
public static class Messages
{
    // Menu
    public const string MenuTitle = "===== Student Management System =====";
    public const string MenuPrompt = "Enter your choice: ";
    public const string InvalidMenuChoice = "Invalid choice. Please select a valid menu option.";
    public const string PressEnterToContinue = "\nPress Enter to continue...";

    // Prompts
    public const string PromptStudentId = "Enter Student ID: ";
    public const string PromptStudentName = "Enter Student Name: ";
    public const string PromptMarks = "Enter Marks (0-100): ";
    public const string PromptDepartmentId = "Enter Department ID: ";
    public const string PromptSearchName = "Enter name (or part of it) to search: ";
    public const string PromptTopN = "How many top students to show? ";
    public const string PromptMinMarks = "Enter minimum marks threshold: ";

    // Success
    public const string StudentAdded = "Student added successfully.";
    public const string StudentUpdated = "Student updated successfully.";
    public const string StudentDeleted = "Student deleted successfully.";

    // Errors / validation
    public const string DuplicateId = "A student with this ID already exists.";
    public const string InvalidMarksRange = "Marks must be between 0 and 100.";
    public const string EmptyName = "Name cannot be empty.";
    public const string InvalidDepartment = "Department does not exist.";
    public const string StudentNotFound = "No student found with that ID.";
    public const string InvalidNumberInput = "Please enter a valid number.";
    public const string NoStudentsFound = "No students found.";

    // Section headers
    public const string HeaderAllStudents = "--- All Students ---";
    public const string HeaderStatistics = "--- Statistics ---";
    public const string HeaderGroupByDepartment = "--- Students Grouped By Department ---";
}
