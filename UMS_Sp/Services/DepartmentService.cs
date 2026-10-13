using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;
using UMS_Sp.Repositories;

namespace UMS_Sp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<DepartmentService> _logger;



        public DepartmentService(IUnitOfWork unitOfWork, IDistributedCache distributedCache, ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
            _logger = logger;
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

            _cache.Remove("departments:all");

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

            _cache.Remove($"department:{id}");
            _cache.Remove("departments:all");

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

            _cache.Remove($"department:{id}");
            _cache.Remove("departments:all");

            return true;
        }

        public async Task<DepartmentWithStudentCountDto?> GetDepartmentWithStudentCountAsync(int id)
        {
            string cacheKey = $"department:studentcount:{id}";

            if (!_cache.TryGetValue(cacheKey, out DepartmentWithStudentCountDto? cachedResult))
            {
                cachedResult = await _unitOfWork.Departments.GetDepartmentWithStudentCountAsync(id);

                if (cachedResult != null)
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(15))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set(cacheKey, cachedResult, cacheOptions);
                }
            }

            return cachedResult;
        }

        public async Task<IEnumerable<DepartmentWithStudentCountDto>> GetAllWithStudentCountAsync()
        {
            string cacheKey = "departments:all:studentcount";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<DepartmentWithStudentCountDto> cachedResults))
            {
                cachedResults = (await _unitOfWork.Departments.GetAllWithStudentCountAsync()).ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, cachedResults, cacheOptions);
            }

            return cachedResults;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            string cacheKey = "departments:all";

            // 1. Cache Check
            if (_cache.TryGetValue(cacheKey, out IEnumerable<DepartmentDto> cachedDepartments))
            {
                _logger.LogInformation("✅ Cache HIT for {Key}", cacheKey);
                return cachedDepartments;
            }

            _logger.LogInformation("❌ Cache MISS for {Key}. Fetching from DB...", cacheKey); 

            var departments = await _unitOfWork.Departments.GetAllAsync();
            cachedDepartments = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                HeadOfDepartment = d.HeadOfDepartment,
                CreatedAt = d.CreatedAt
            }).ToList();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            _cache.Set(cacheKey, cachedDepartments, cacheOptions);

            return cachedDepartments;
        }
        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"department:{id}";

            // 1. Cache Check
            if (_cache.TryGetValue(cacheKey, out DepartmentDto? cachedDepartment))
            {
                _logger.LogInformation("✅ Cache HIT for {Key}", cacheKey); 
                return cachedDepartment;
            }

            _logger.LogInformation("❌ Cache MISS for {Key}. Fetching from DB...", cacheKey);

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

            return cachedDepartment;
        }

        // --- Helper Methods for Serialization ---
        private async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await _distributedCache.GetAsync(key);
            if (cachedData == null) return default;
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        private async Task SetCacheAsync<T>(string key, T value, DistributedCacheEntryOptions options)
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(value);
            await _distributedCache.SetAsync(key, bytes, options);
        }

    }
}
