using Auth.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Auth.Models
{
    public class ApplicationUser : IdentityUser<string>, ITenantEntity
    {
        public string TenantId { get; set; } = string.Empty;
        public Tenant? Tenant { get; set; }

        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
