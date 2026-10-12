using Auth.Services.Interfaces;

namespace Auth.Services
{
    public class TenantContext : ITenantContext
    {
        public string? TenantId { get; private set; }

        public void SetTenant(string tenantId)
        {
            TenantId = tenantId;
        }
    }
}
