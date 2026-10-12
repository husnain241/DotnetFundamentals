using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<(IEnumerable<Course> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination);
        Task<Course?> GetByIdWithDepartmentAsync(int id);
        Task<IEnumerable<Course>> GetCoursesByDepartmentAsync(int departmentId);
        Task<CourseWithStudentCountDto?> GetCourseWithStudentCountAsync(int id);
        Task<IEnumerable<CourseWithStudentCountDto>> GetAllWithStudentCountAsync();
    }
}
