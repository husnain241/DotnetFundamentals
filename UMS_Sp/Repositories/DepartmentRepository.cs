using Microsoft.EntityFrameworkCore;
using UMS_Sp.Data;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;

namespace UMS_Sp.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Department> Items, int TotalCount)> GetPagedAsync(PaginationParams pagination)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.SearchTerm))
            {
                var term = pagination.SearchTerm.Trim().ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(term) || d.HeadOfDepartment.ToLower().Contains(term));
            }

            // Sorting
            query = pagination.SortBy?.ToLower() switch
            {
                "name" => pagination.IsDescending ? query.OrderByDescending(d => d.Name) : query.OrderBy(d => d.Name),
                "headofdepartment" => pagination.IsDescending ? query.OrderByDescending(d => d.HeadOfDepartment) : query.OrderBy(d => d.HeadOfDepartment),
                "createdat" => pagination.IsDescending ? query.OrderByDescending(d => d.CreatedAt) : query.OrderBy(d => d.CreatedAt),
                _ => pagination.IsDescending ? query.OrderByDescending(d => d.Id) : query.OrderBy(d => d.Id)
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                   .Take(pagination.PageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Department?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(d => d.Students)
                .Include(d => d.Courses)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DepartmentWithStudentCountDto?> GetDepartmentWithStudentCountAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DepartmentWithStudentCountDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    HeadOfDepartment = d.HeadOfDepartment,
                    CreatedAt = d.CreatedAt,
                    TotalStudents = d.Students.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DepartmentWithStudentCountDto>> GetAllWithStudentCountAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Select(d => new DepartmentWithStudentCountDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    HeadOfDepartment = d.HeadOfDepartment,
                    CreatedAt = d.CreatedAt,
                    TotalStudents = d.Students.Count
                })
                .ToListAsync();
        }
    }
}
