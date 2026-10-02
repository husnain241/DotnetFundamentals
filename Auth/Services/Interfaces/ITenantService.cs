using Auth.DTOs.Tenant;

namespace Auth.Services.Interfaces
{
    public interface ITenantService
    {
        Task<TenantDto> CreateTenantAsync(CreateTenantDto dto);
        Task<IEnumerable<TenantDto>> GetAllTenantsAsync();
    }
}
