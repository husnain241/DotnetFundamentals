namespace Auth.Services.Interfaces
{
    public interface ITenantContext
    {
        string? TenantId { get; }
        void SetTenant(string tenantId);
    }
}
