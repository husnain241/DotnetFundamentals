using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<(IEnumerable<Student> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination);
        Task<Student?> GetByIdWithDepartmentAsync(int id);
        Task<IEnumerable<Student>> GetStudentsByDepartmentAsync(int departmentId);
        Task<Student?> GetStudentWithCoursesAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeStudentId = null);

        Task<Student> GetByIdUsingStoredProcedureAsync(int id);
        Task<StudentDepartmentDto> GetStudentWithDepartmentAsync(int id);

    }
}
