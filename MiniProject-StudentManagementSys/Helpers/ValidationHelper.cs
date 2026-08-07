namespace MiniProject_StudentManagementSys.Helpers;

public static class ValidationHelper
{
    public static bool IsNameValid(string? name)
        => !string.IsNullOrWhiteSpace(name);

    public static bool IsMarksInRange(double marks)
        => marks is >= 0 and <= 100;

    public static bool IsPositiveId(int id)
        => id > 0;
}
