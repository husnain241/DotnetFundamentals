using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Auth.Services.Identity
{
    public class TenantUserValidator : IUserValidator<ApplicationUser>
    {
        public IdentityErrorDescriber Describer { get; }

        public TenantUserValidator(IdentityErrorDescriber? errors = null)
        {
            Describer = errors ?? new IdentityErrorDescriber();
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            ArgumentNullException.ThrowIfNull(manager);
            ArgumentNullException.ThrowIfNull(user);

            var errors = new List<IdentityError>();

            await ValidateUserName(manager, user, errors);

            if (manager.Options.User.RequireUniqueEmail)
            {
                await ValidateEmail(manager, user, errors);
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        private async Task ValidateUserName(UserManager<ApplicationUser> manager, ApplicationUser user, List<IdentityError> errors)
        {
            var userName = await manager.GetUserNameAsync(user);
            if (string.IsNullOrWhiteSpace(userName))
            {
                errors.Add(Describer.InvalidUserName(userName));
                return;
            }

            if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) &&
                userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
            {
                errors.Add(Describer.InvalidUserName(userName));
                return;
            }

            // Tenant-scoped uniqueness check for UserName
            var normalizedUserName = manager.NormalizeName(userName);
            if (string.IsNullOrEmpty(normalizedUserName))
            {
                errors.Add(Describer.InvalidUserName(userName));
                return;
            }

            var query = manager.Users.Where(u => u.NormalizedUserName == normalizedUserName);

            if (string.IsNullOrEmpty(user.TenantId))
            {
                query = query.Where(u => u.TenantId == null || u.TenantId == string.Empty);
            }
            else
            {
                query = query.Where(u => u.TenantId == user.TenantId);
            }

            var owner = await query.FirstOrDefaultAsync();
            if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(Describer.DuplicateUserName(userName));
            }
        }

        private async Task ValidateEmail(UserManager<ApplicationUser> manager, ApplicationUser user, List<IdentityError> errors)
        {
            var email = await manager.GetEmailAsync(user);
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(Describer.InvalidEmail(email));
                return;
            }

            if (!new EmailAddressAttribute().IsValid(email))
            {
                errors.Add(Describer.InvalidEmail(email));
                return;
            }

            // Tenant-scoped uniqueness check for Email
            var normalizedEmail = manager.NormalizeEmail(email);
            if (string.IsNullOrEmpty(normalizedEmail))
            {
                errors.Add(Describer.InvalidEmail(email));
                return;
            }

            var query = manager.Users.Where(u => u.NormalizedEmail == normalizedEmail);

            if (string.IsNullOrEmpty(user.TenantId))
            {
                query = query.Where(u => u.TenantId == null || u.TenantId == string.Empty);
            }
            else
            {
                query = query.Where(u => u.TenantId == user.TenantId);
            }

            var owner = await query.FirstOrDefaultAsync();
            if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(Describer.DuplicateEmail(email));
            }
        }
    }
}
