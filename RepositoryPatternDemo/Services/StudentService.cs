using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
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
    }
}