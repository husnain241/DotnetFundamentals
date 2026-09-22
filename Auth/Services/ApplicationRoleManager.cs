using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Auth.Services
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(
            IRoleStore<IdentityRole> store,
            IEnumerable<IRoleValidator<IdentityRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<IdentityRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        // 1. Custom Method: Create Role with System Protection Flag / Business Validation
        public async Task<IdentityResult> CreateCustomRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRoleName",
                    Description = "Role name cannot be empty."
                });
            }

            var roleExists = await RoleExistsAsync(roleName);
            if (roleExists)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleExists",
                    Description = $"Role '{roleName}' already exists in the system."
                });
            }

            return await CreateAsync(new IdentityRole(roleName.Trim()));
        }

        // 2. Custom Method: Safe Delete Role (Prevent deletion of core system roles)
        public async Task<IdentityResult> DeleteRoleSafelyAsync(string roleName)
        {
            var protectedRoles = new[] { "Admin", "User", "Manager" };

            if (protectedRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "ProtectedRole",
                    Description = $"Role '{roleName}' is a core system role and cannot be deleted."
                });
            }

            var role = await FindByNameAsync(roleName);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = $"Role '{roleName}' does not exist."
                });
            }

            return await DeleteAsync(role);
        }
    }
}