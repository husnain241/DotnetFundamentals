using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);

        IEnumerable<Student> GetAllAsNoTracking();

        IEnumerable<Student> GetAllWithDepartment();


        Task<Student?> GetByIdCompiledAsync(int id);
        Task<List<Student>> GetByDepartmentCompiledAsync(int departmentId);
    }
}