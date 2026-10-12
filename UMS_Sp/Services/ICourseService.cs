using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;

namespace UMS_Sp.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<PagedResult<CourseDto>> GetPagedAsync(PaginationParams pagination);
        Task<CourseDto?> GetByIdAsync(int id);
        Task<CourseDto> CreateAsync(CreateCourseDto dto);
        Task<CourseDto?> UpdateAsync(int id, UpdateCourseDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CourseDto>> GetCoursesByDepartmentAsync(int departmentId);
        Task<CourseWithStudentCountDto?> GetCourseWithStudentCountAsync(int id);
        Task<IEnumerable<CourseWithStudentCountDto>> GetAllWithStudentCountAsync();
    }
}
