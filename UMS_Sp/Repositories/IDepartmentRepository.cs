using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<(IEnumerable<Department> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination);
        Task<Department?> GetByIdWithDetailsAsync(int id);
        Task<DepartmentWithStudentCountDto?> GetDepartmentWithStudentCountAsync(int id);
        Task<IEnumerable<DepartmentWithStudentCountDto>> GetAllWithStudentCountAsync();
    }
}
