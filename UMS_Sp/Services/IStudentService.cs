using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;

namespace UMS_Sp.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<PagedResult<StudentDto>> GetPagedAsync(PaginationParams pagination);
        Task<StudentDetailDto?> GetByIdWithDepartmentAsync(int id);
        Task<StudentDto> CreateAsync(CreateStudentDto dto);
        Task<StudentDto?> UpdateAsync(int id, UpdateStudentDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId);
        Task<StudentWithCoursesDto?> GetStudentWithCoursesAsync(int id);

        Task<StudentDto?> GetByIdUsingStoredProcedureAsync(int id);
    }
}
