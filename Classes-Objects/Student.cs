
public class Student
{
    public int RegId;
    public string FullName;
    public string University;

    public Student()
    {
        RegId = 0;
        FullName = "Unknown";
        University = "Unknown";
    }

    public Student(int id, string fullName, string university)
    {
        this.RegId = id;
        this.FullName = fullName;
        this.University = university;
    }
}
