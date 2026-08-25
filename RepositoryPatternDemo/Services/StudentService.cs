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
            var students = _unitOfWork.Students.GetAll();
            foreach (var student in students)
            {
                PopulateDepartment(student);
            }
            return students;
        }

        public Student? GetById(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student != null)
            {
                PopulateDepartment(student);
            }
            return student;
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

            student.DepartmentId = departmentId;
            student.Department = department;

            _unitOfWork.Students.Add(student);
            _unitOfWork.Save();

            Console.WriteLine(
                $"Student {student.Name} added to department {department.Name}");
        }

        private void PopulateDepartment(Student student)
        {
            if (student.DepartmentId.HasValue)
            {
                student.Department = _unitOfWork.Departments.GetById(student.DepartmentId.Value);
            }
        }
    }
}