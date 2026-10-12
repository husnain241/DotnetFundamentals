using Auth.Models; // ApplicationRole ki namespace include karein
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Auth.Services.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(
            IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

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

            // Create ApplicationRole instance instead of IdentityRole
            return await CreateAsync(new ApplicationRole { Name = roleName.Trim() });
        }

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