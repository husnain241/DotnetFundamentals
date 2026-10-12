using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<(IEnumerable<Enrollment> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination);
        Task<Enrollment?> GetByIdWithDetailsAsync(int id);
        Task<EnrollmentDetailDto?> GetEnrollmentDetailsAsync(int id);
        Task<IEnumerable<EnrollmentDetailDto>> GetAllDetailsAsync();
        Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<bool> IsEnrolledAsync(int studentId, int courseId);
    }
}
