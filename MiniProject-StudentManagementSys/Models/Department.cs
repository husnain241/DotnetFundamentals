namespace MiniProject_StudentManagementSys.Models;


public class Department
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Department(int id, string name)
    {
        Id = id;
        Name = name;
    }
    //if we need crud related to department we can add methods here like update name etc
}
