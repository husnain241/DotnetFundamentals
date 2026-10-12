namespace Auth.Services.Interfaces
{
    public interface ITenantProvider
    {
        string? GetTenantId();
    }
}
