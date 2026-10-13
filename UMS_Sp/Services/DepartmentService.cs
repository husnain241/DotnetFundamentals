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

            // Cache Invalidation: List cache ko remove karein
            await _distributedCache.RemoveAsync("departments:all");

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

            // Cache Invalidation: Specific aur List dono ko remove karein
            await _distributedCache.RemoveAsync($"department:{id}");
            await _distributedCache.RemoveAsync("departments:all");

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

            // Cache Invalidation: Specific aur List dono ko remove karein
            await _distributedCache.RemoveAsync($"department:{id}");
            await _distributedCache.RemoveAsync("departments:all");

            return true;
        }

        public async Task<DepartmentWithStudentCountDto?> GetDepartmentWithStudentCountAsync(int id)
        {
            string cacheKey = $"department:studentcount:{id}";

            // 1. Cache Check (Helper method use kiya)
            var cachedResult = await GetFromCacheAsync<DepartmentWithStudentCountDto>(cacheKey);

            if (cachedResult != null)
            {
                _logger.LogInformation("✅ Distributed Cache HIT for {Key}", cacheKey);
                return cachedResult;
            }

            _logger.LogInformation("❌ Distributed Cache MISS for {Key}. Fetching from DB...", cacheKey);

            // 2. Cache Miss: Database se heavy query load karein
            cachedResult = await _unitOfWork.Departments.GetDepartmentWithStudentCountAsync(id);

            if (cachedResult != null)
            {
                // 3. Cache Options (Heavy queries ke liye thora lamba time)
                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                // 4. Cache mein store karein
                await SetCacheAsync(cacheKey, cachedResult, cacheOptions);
            }

            return cachedResult;
        }

        public async Task<IEnumerable<DepartmentWithStudentCountDto>> GetAllWithStudentCountAsync()
        {
            string cacheKey = "departments:all:studentcount";

            // 1. Cache Check (Helper method use kiya)
            // Note: Hum yahan List<DepartmentWithStudentCountDto> use kar rahe hain serialization ke liye
            var cachedResults = await GetFromCacheAsync<List<DepartmentWithStudentCountDto>>(cacheKey);

            if (cachedResults != null)
            {
                _logger.LogInformation("✅ Distributed Cache HIT for {Key}", cacheKey);
                return cachedResults;
            }

            _logger.LogInformation("❌ Distributed Cache MISS for {Key}. Fetching from DB...", cacheKey);

            // 2. Cache Miss: Database se heavy query load karein
            // .ToList() zaroori hai taake data serialize ho sake
            cachedResults = (await _unitOfWork.Departments.GetAllWithStudentCountAsync()).ToList();

            // 3. Cache Options
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15))
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            // 4. Cache mein store karein
            await SetCacheAsync(cacheKey, cachedResults, cacheOptions);

            return cachedResults;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            string cacheKey = "departments:all";

            // 1. Cache Check (Helper method use kiya)
            var cachedDepartments = await GetFromCacheAsync<List<DepartmentDto>>(cacheKey);

            if (cachedDepartments != null)
            {
                _logger.LogInformation("✅ Distributed Cache HIT for {Key}", cacheKey);
                return cachedDepartments;
            }

            _logger.LogInformation("❌ Distributed Cache MISS for {Key}. Fetching from DB...", cacheKey);

            // 2. Cache Miss: Database se data load karein
            var departments = await _unitOfWork.Departments.GetAllAsync();

            // ️ Important: .ToList() lagana zaroori hai taake data memory mein materialize ho jaye
            var departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                HeadOfDepartment = d.HeadOfDepartment,
                CreatedAt = d.CreatedAt
            }).ToList();

            // 3. Cache Options set karein
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            // 4. Cache mein store karein (Helper method use kiya)
            await SetCacheAsync(cacheKey, departmentDtos, cacheOptions);

            return departmentDtos;
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"department:{id}";

            // 1. Cache Check
            var cachedDepartment = await GetFromCacheAsync<DepartmentDto>(cacheKey);

            if (cachedDepartment != null)
            {
                _logger.LogInformation("✅ Distributed Cache HIT for {Key}", cacheKey);
                return cachedDepartment;
            }

            _logger.LogInformation("❌ Distributed Cache MISS for {Key}. Fetching from DB...", cacheKey);

            // 2. Cache Miss: Database se load karein
            var department = await _unitOfWork.Departments.GetByIdAsync(id);

            if (department != null)
            {
                var departmentDto = new DepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    HeadOfDepartment = department.HeadOfDepartment,
                    CreatedAt = department.CreatedAt
                };

                // 3. Cache Options
                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                // 4. Cache mein store karein
                await SetCacheAsync(cacheKey, departmentDto, cacheOptions);

                return departmentDto;
            }

            return null;
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
