using Auth.Services.Interfaces;

namespace Auth.Services
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetTenantId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return null;

            // 1. Try resolving from Request Header "X-Tenant-Id"
            if (httpContext.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader) && !string.IsNullOrWhiteSpace(tenantHeader))
            {
                return tenantHeader.ToString();
            }

            // 2. Try resolving from JWT Token Claims ("tenant_id" or "TenantId")
            var tenantClaim = httpContext.User?.FindFirst("tenant_id")?.Value
                           ?? httpContext.User?.FindFirst("TenantId")?.Value;

            if (!string.IsNullOrWhiteSpace(tenantClaim))
            {
                return tenantClaim;
            }

            return null;
        }
    }
}