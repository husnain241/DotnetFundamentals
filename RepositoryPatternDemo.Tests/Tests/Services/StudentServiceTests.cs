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


    // =========================================================
    // GetById()
    // =========================================================

    [Fact]
    public void GetById_ShouldReturnStudent_WhenStudentExists()
    {
        // Arrange
        var student = new Student
        {
            Id = 1,
            Name = "Ali"
        };

        _studentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(student);

        // Act
        var result = _studentService.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Ali", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenStudentDoesNotExist()
    {
        // Arrange
        _studentRepositoryMock
            .Setup(x => x.GetById(99))
            .Returns((Student?)null);

        // Act
        var result = _studentService.GetById(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetById_ShouldPopulateDepartment_WhenStudentHasDepartment()
    {
        // Arrange
        var department = new Department
        {
            Id = 1,
            Name = "Computer Science"
        };

        var student = new Student
        {
            Id = 1,
            Name = "Ali",
            DepartmentId = 1
        };

        _studentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(student);

        _departmentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(department);

        // Act
        var result = _studentService.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Department);
        Assert.Equal(1, result.Department.Id);
        Assert.Equal("Computer Science", result.Department.Name);
    }
    // =========================================================
    // Update()
    // =========================================================

    [Fact]
    public void Update_ShouldUpdateStudent()
    {
        // Arrange
        var student = new Student
        {
            Id = 1,
            Name = "Ali Updated"
        };

        // Act
        _studentService.Update(student);

        // Assert
        _studentRepositoryMock.Verify(
            x => x.Update(student),
            Times.Once);
    }

    [Fact]
    public void Update_ShouldCallComplete()
    {
        // Arrange
        var student = new Student
        {
            Id = 1,
            Name = "Ali Updated"
        };

        // Act
        _studentService.Update(student);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.Complete(),
            Times.Once);
    }
    // =========================================================
    // Delete()
    // =========================================================

    [Fact]
    public void Delete_ShouldDeleteStudent_WhenStudentExists()
    {
        // Arrange
        var student = new Student
        {
            Id = 1,
            Name = "Ali"
        };

        _studentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(student);

        // Act
        _studentService.Delete(1);

        // Assert
        _studentRepositoryMock.Verify(
            x => x.Delete(1),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.Complete(),
            Times.Once);
    }

    [Fact]
    public void Delete_ShouldDecreaseDepartmentStudentCount_WhenStudentHasDepartment()
    {
        // Arrange
        var department = new Department
        {
            Id = 1,
            Name = "Computer Science",
            StudentCount = 5
        };

        var student = new Student
        {
            Id = 1,
            Name = "Ali",
            DepartmentId = 1
        };

        _studentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(student);

        _departmentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(department);

        // Act
        _studentService.Delete(1);

        // Assert
        Assert.Equal(4, department.StudentCount);

        _departmentRepositoryMock.Verify(
            x => x.Update(department),
            Times.Once);
    }

    [Fact]
    public void Delete_ShouldDoNothing_WhenStudentDoesNotExist()
    {
        // Arrange
        _studentRepositoryMock
            .Setup(x => x.GetById(99))
            .Returns((Student?)null);

        // Act
        _studentService.Delete(99);

        // Assert
        _studentRepositoryMock.Verify(
            x => x.Delete(99),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.Complete(),
            Times.Never);
    }

    [Fact]
    public void Delete_ShouldCallComplete_WhenStudentExists()
    {
        // Arrange
        var student = new Student
        {
            Id = 1,
            Name = "Ali"
        };

        _studentRepositoryMock
            .Setup(x => x.GetById(1))
            .Returns(student);

        // Act
        _studentService.Delete(1);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.Complete(),
            Times.Once);
    }



}