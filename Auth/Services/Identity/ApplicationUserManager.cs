using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Auth.Services.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        // 1. Custom Method: Find user by email only if active / validated
        public async Task<ApplicationUser?> FindActiveByEmailAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            if (user != null && user.EmailConfirmed)
            {
                return user;
            }
            return null;
        }

        // 2. Custom Method: Soft Deactivate User (Lockout)
        public async Task<IdentityResult> DeactivateUserAsync(ApplicationUser user)
        {
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // Indefinite lockout

            return await UpdateAsync(user);
        }

        // 3. Custom Method: Find user by username scoped to tenant
        public async Task<ApplicationUser?> FindByNameAndTenantAsync(string userName, string tenantId)
        {
            var normalizedUserName = NormalizeName(userName);
            return await Users.FirstOrDefaultAsync(u => u.TenantId == tenantId && u.NormalizedUserName == normalizedUserName);
        }

        // 4. Custom Method: Find user by email scoped to tenant
        public async Task<ApplicationUser?> FindByEmailAndTenantAsync(string email, string tenantId)
        {
            var normalizedEmail = NormalizeEmail(email);
            return await Users.FirstOrDefaultAsync(u => u.TenantId == tenantId && u.NormalizedEmail == normalizedEmail);
        }
    }
}