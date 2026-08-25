using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Student> GetAll()
        {
            return _unitOfWork.Students.GetAll();
        }

        public Student? GetById(int id)
        {
            return _unitOfWork.Students.GetById(id);
        }

        public void Add(Student student)
        {
            _unitOfWork.Students.Add(student);
            _unitOfWork.Save();
        }

        public void Update(Student student)
        {
            _unitOfWork.Students.Update(student);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Students.Delete(id);
            _unitOfWork.Save();
        }

        public void AddStudentWithDepartment(Student student, int departmentId)
        {
            var department = _unitOfWork.Departments.GetById(departmentId);

            if (department == null)
            {
                throw new Exception("Department not found.");
            }

            _unitOfWork.Students.Add(student);
            _unitOfWork.Save();

            Console.WriteLine(
                $"Student {student.Name} added to department {department.Name}");
        }
    }
}