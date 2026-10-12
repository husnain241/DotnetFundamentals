using Microsoft.EntityFrameworkCore;
using UMS_Sp.Data;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Student> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination)
        {
            var query = _dbSet.AsNoTracking().Include(s => s.Department).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.SearchTerm))
            {
                var term = pagination.SearchTerm.Trim().ToLower();
                query = query.Where(s => s.Name.ToLower().Contains(term) || s.Email.ToLower().Contains(term));
            }

            // Sorting
            query = pagination.SortBy?.ToLower() switch
            {
                "name" => pagination.IsDescending ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name),
                "email" => pagination.IsDescending ? query.OrderByDescending(s => s.Email) : query.OrderBy(s => s.Email),
                "enrollmentdate" => pagination.IsDescending ? query.OrderByDescending(s => s.EnrollmentDate) : query.OrderBy(s => s.EnrollmentDate),
                _ => pagination.IsDescending ? query.OrderByDescending(s => s.Id) : query.OrderBy(s => s.Id)
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                   .Take(pagination.PageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Student?> GetByIdWithDepartmentAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetStudentsByDepartmentAsync(int departmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(s => s.Department)
                .Where(s => s.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentWithCoursesAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(s => s.Department)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeStudentId = null)
        {
            var normalizedEmail = email.Trim().ToLower();
            return await _dbSet.AnyAsync(s => s.Email.ToLower() == normalizedEmail && (!excludeStudentId.HasValue || s.Id != excludeStudentId.Value));
        }
    }
}
