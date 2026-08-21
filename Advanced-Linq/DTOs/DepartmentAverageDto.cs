namespace Advanced_Linq.DTOs
{
    public class DepartmentAverageDto
    {
        public string Department { get; set; } = string.Empty;
        public double AverageMarks { get; set; }
        public int StudentCount { get; set; }
    }
}