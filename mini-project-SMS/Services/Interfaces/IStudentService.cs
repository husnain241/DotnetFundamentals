using mini_project_SMS.Models;

namespace mini_project_SMS.Services.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();

        IEnumerable<Student> Search(string searchTerm);

        Student? GetById(int id);

        void Add(Student student);

        bool Update(Student student);

        bool Delete(int id);
    }
}
