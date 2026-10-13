using Microsoft.Extensions.Caching.Memory;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;
using UMS_Sp.Repositories;

namespace UMS_Sp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;


        public DepartmentService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<PagedResult<DepartmentDto>> GetPagedAsync(PaginationParams pagination)
        {
            var (items, totalCount) = await _unitOfWork.Departments.GetPagedAsync(pagination);
            var dtos = items.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                HeadOfDepartment = d.HeadOfDepartment,
                CreatedAt = d.CreatedAt
            });

            return new PagedResult<DepartmentDto>(dtos, totalCount, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                HeadOfDepartment = dto.HeadOfDepartment,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CompleteAsync();

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                HeadOfDepartment = department.HeadOfDepartment,
                CreatedAt = department.CreatedAt
            };
        }

        public async Task<DepartmentDto?> UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null) return null;

            department.Name = dto.Name;
            department.HeadOfDepartment = dto.HeadOfDepartment;

            _unitOfWork.Departments.Update(department);
            await _unitOfWork.CompleteAsync();

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                HeadOfDepartment = department.HeadOfDepartment,
                CreatedAt = department.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null) return false;

            _unitOfWork.Departments.Delete(department);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<DepartmentWithStudentCountDto?> GetDepartmentWithStudentCountAsync(int id)
        {
            return await _unitOfWork.Departments.GetDepartmentWithStudentCountAsync(id);
        }

        public async Task<IEnumerable<DepartmentWithStudentCountDto>> GetAllWithStudentCountAsync()
        {
            return await _unitOfWork.Departments.GetAllWithStudentCountAsync();
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            string cacheKey = "departments:all";

            // 1. Cache Check
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<DepartmentDto> cachedDepartments))
            {
                var departments = await _unitOfWork.Departments.GetAllAsync();
                cachedDepartments = departments.Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    HeadOfDepartment = d.HeadOfDepartment,
                    CreatedAt = d.CreatedAt
                }).ToList(); 

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)) // Max 10 minute
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));  // 2 minute idle hone par expire

                _cache.Set(cacheKey, cachedDepartments, cacheOptions);
            }

            return cachedDepartments;
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"department:{id}";

            // 1. Cache Check
            if (!_cache.TryGetValue(cacheKey, out DepartmentDto? cachedDepartment))
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);

                if (department != null)
                {
                    cachedDepartment = new DepartmentDto
                    {
                        Id = department.Id,
                        Name = department.Name,
                        HeadOfDepartment = department.HeadOfDepartment,
                        CreatedAt = department.CreatedAt
                    };

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    _cache.Set(cacheKey, cachedDepartment, cacheOptions);
                }
            }

            return cachedDepartment;
        }
    }
}
