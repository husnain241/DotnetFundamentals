using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;

namespace UMS_Sp.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<PagedResult<DepartmentDto>> GetPagedAsync(PaginationParams pagination);
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto);
        Task<DepartmentDto?> UpdateAsync(int id, UpdateDepartmentDto dto);
        Task<bool> DeleteAsync(int id);
        Task<DepartmentWithStudentCountDto?> GetDepartmentWithStudentCountAsync(int id);
        Task<IEnumerable<DepartmentWithStudentCountDto>> GetAllWithStudentCountAsync();
    }
}
