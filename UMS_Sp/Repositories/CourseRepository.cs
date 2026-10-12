using Microsoft.EntityFrameworkCore;
using UMS_Sp.Data;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Course> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination)
        {
            var query = _dbSet.AsNoTracking().Include(c => c.Department).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.SearchTerm))
            {
                var term = pagination.SearchTerm.Trim().ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(term) || (c.Department != null && c.Department.Name.ToLower().Contains(term)));
            }

            // Sorting
            query = pagination.SortBy?.ToLower() switch
            {
                "title" => pagination.IsDescending ? query.OrderByDescending(c => c.Title) : query.OrderBy(c => c.Title),
                "credits" => pagination.IsDescending ? query.OrderByDescending(c => c.Credits) : query.OrderBy(c => c.Credits),
                "department" => pagination.IsDescending ? query.OrderByDescending(c => c.Department != null ? c.Department.Name : "") : query.OrderBy(c => c.Department != null ? c.Department.Name : ""),
                _ => pagination.IsDescending ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id)
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                   .Take(pagination.PageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Course?> GetByIdWithDepartmentAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByDepartmentAsync(int departmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Department)
                .Where(c => c.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<CourseWithStudentCountDto?> GetCourseWithStudentCountAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CourseWithStudentCountDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Credits = c.Credits,
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.Department != null ? c.Department.Name : string.Empty,
                    EnrolledStudentsCount = c.Enrollments.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CourseWithStudentCountDto>> GetAllWithStudentCountAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Select(c => new CourseWithStudentCountDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Credits = c.Credits,
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.Department != null ? c.Department.Name : string.Empty,
                    EnrolledStudentsCount = c.Enrollments.Count
                })
                .ToListAsync();
        }
    }
}
