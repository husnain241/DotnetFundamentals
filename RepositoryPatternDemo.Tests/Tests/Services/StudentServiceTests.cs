using Moq;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services;

public class StudentServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly StudentService _studentService;

    public StudentServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _studentRepositoryMock = new Mock<IStudentRepository>();
        _departmentRepositoryMock = new Mock<IDepartmentRepository>();
        _studentService = new StudentService(_unitOfWorkMock.Object);

        _unitOfWorkMock
            .Setup(x => x.Students)
            .Returns(_studentRepositoryMock.Object);

        _unitOfWorkMock
            .Setup(x => x.Departments)
            .Returns(_departmentRepositoryMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnStudents_WhenStudentsExist()
    {
        // Arrange
        var students = new List<Student>
    {
        new Student { Id = 1, Name = "Ali" ,},
        new Student { Id = 2, Name = "Ahmed" }
    };

        _studentRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(students);

        // Act
        var result = _studentService.GetAll();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Ali", result[0].Name);
        Assert.Equal("Ahmed", result[1].Name);
    }

    [Fact]
    public void GetAll_ShouldReturnEmptyList_WhenNoStudentsExist()
    {
        _studentRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(new List<Student>());

        var result = _studentService.GetAll();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetById_ShouldReturnStudent_WhenStudentExists()
    {
        // Arrange
        var testStudent = new Student { Id = 1, Name = "Ali" };

        _studentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(testStudent);

        // Act
        var result = _studentService.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Ali", result.Name);
    }
}