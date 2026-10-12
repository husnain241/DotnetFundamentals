using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;

namespace UMS_Sp.Services
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDetailDto>> GetAllAsync();
        Task<PagedResult<EnrollmentDetailDto>> GetPagedAsync(PaginationParams pagination);
        Task<EnrollmentDetailDto?> GetByIdAsync(int id);
        Task<EnrollmentDetailDto> EnrollStudentAsync(CreateEnrollmentDto dto);
        Task<EnrollmentDetailDto?> UpdateEnrollmentAsync(int id, UpdateEnrollmentDto dto);
        Task<bool> DeleteEnrollmentAsync(int id);
        Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByCourseIdAsync(int courseId);
    }
}
