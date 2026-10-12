using Microsoft.EntityFrameworkCore;
using UMS_Sp.Data;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Enrollment> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination)
        {
            var query = _dbSet.AsNoTracking()
                              .Include(e => e.Student)
                              .Include(e => e.Course)
                              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.SearchTerm))
            {
                var term = pagination.SearchTerm.Trim().ToLower();
                query = query.Where(e => (e.Student != null && e.Student.Name.ToLower().Contains(term)) ||
                                         (e.Course != null && e.Course.Title.ToLower().Contains(term)) ||
                                         (e.Grade != null && e.Grade.ToLower().Contains(term)));
            }

            // Sorting
            query = pagination.SortBy?.ToLower() switch
            {
                "enrollmentdate" => pagination.IsDescending ? query.OrderByDescending(e => e.EnrollmentDate) : query.OrderBy(e => e.EnrollmentDate),
                "grade" => pagination.IsDescending ? query.OrderByDescending(e => e.Grade) : query.OrderBy(e => e.Grade),
                "marks" => pagination.IsDescending ? query.OrderByDescending(e => e.Marks) : query.OrderBy(e => e.Marks),
                "student" => pagination.IsDescending ? query.OrderByDescending(e => e.Student != null ? e.Student.Name : "") : query.OrderBy(e => e.Student != null ? e.Student.Name : ""),
                "course" => pagination.IsDescending ? query.OrderByDescending(e => e.Course != null ? e.Course.Title : "") : query.OrderBy(e => e.Course != null ? e.Course.Title : ""),
                _ => pagination.IsDescending ? query.OrderByDescending(e => e.Id) : query.OrderBy(e => e.Id)
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                   .Take(pagination.PageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Enrollment?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EnrollmentDetailDto?> GetEnrollmentDetailsAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new EnrollmentDetailDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    StudentName = e.Student != null ? e.Student.Name : string.Empty,
                    StudentEmail = e.Student != null ? e.Student.Email : string.Empty,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course != null ? e.Course.Title : string.Empty,
                    CourseCredits = e.Course != null ? e.Course.Credits : 0,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade,
                    Marks = e.Marks
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EnrollmentDetailDto>> GetAllDetailsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Select(e => new EnrollmentDetailDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    StudentName = e.Student != null ? e.Student.Name : string.Empty,
                    StudentEmail = e.Student != null ? e.Student.Email : string.Empty,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course != null ? e.Course.Title : string.Empty,
                    CourseCredits = e.Course != null ? e.Course.Credits : 0,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade,
                    Marks = e.Marks
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(e => e.StudentId == studentId)
                .Select(e => new EnrollmentDetailDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    StudentName = e.Student != null ? e.Student.Name : string.Empty,
                    StudentEmail = e.Student != null ? e.Student.Email : string.Empty,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course != null ? e.Course.Title : string.Empty,
                    CourseCredits = e.Course != null ? e.Course.Credits : 0,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade,
                    Marks = e.Marks
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(e => e.CourseId == courseId)
                .Select(e => new EnrollmentDetailDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    StudentName = e.Student != null ? e.Student.Name : string.Empty,
                    StudentEmail = e.Student != null ? e.Student.Email : string.Empty,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course != null ? e.Course.Title : string.Empty,
                    CourseCredits = e.Course != null ? e.Course.Credits : 0,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade,
                    Marks = e.Marks
                })
                .ToListAsync();
        }

        public async Task<bool> IsEnrolledAsync(int studentId, int courseId)
        {
            return await _dbSet.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
