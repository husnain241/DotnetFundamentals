using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public StudentService(IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
        }

        public List<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student? GetById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public void Add(Student student)
        {
            _studentRepository.Add(student);
        }

        public void Update(Student student)
        {
            _studentRepository.Update(student);
        }

        public void Delete(int id)
        {
            _studentRepository.Delete(id);
        }

        ///testing 
        public void AddStudentWithDepartment(Student student, int departmentId)
        {
            var department = _departmentRepository.GetById(departmentId);

            if (department == null)
            {
                throw new Exception("Department not found.");
            }

            _studentRepository.Add(student);

            Console.WriteLine(
                $"Student {student.Name} added to department {department.Name}");
        }
    }
}