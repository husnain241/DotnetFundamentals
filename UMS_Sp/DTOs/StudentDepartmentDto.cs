namespace UMS_Sp.DTOs
{
    public class StudentDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; } // Yeh JOIN se aayega
    }
}