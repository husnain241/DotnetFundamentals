using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
        void TransferDepartment(int studentId, int newDepartmentId);

        IEnumerable<Student> GetAllAsNoTracking();
        IEnumerable<Student> GetAllWithDepartment();
        void TransferDepartmentByTransaction(int studentId, int newDepartmentId);

        // Bulk Operations
        void BulkInsertStudents(IEnumerable<Student> students);
        int BulkUpdateDepartmentId(int oldDepartmentId, int newDepartmentId);
        int BulkDeleteByDepartmentId(int departmentId);
    }
}