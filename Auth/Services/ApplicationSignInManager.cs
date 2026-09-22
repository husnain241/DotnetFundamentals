using Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Auth.Services
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationSignInManager(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> userConfirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, userConfirmation)
        {
            _userManager = userManager;
        }

        // Custom Extended Sign-In Method with Pre-Authentication Account Checks
        public async Task<SignInResult> CustomPasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            // Custom Rule: Check if account is deactivated / locked out BEFORE checking password
            if (await _userManager.IsLockedOutAsync(user))
            {
                return SignInResult.LockedOut;
            }

            // Custom Rule: Check if user's email is confirmed
            if (!user.EmailConfirmed)
            {
                return SignInResult.NotAllowed;
            }

            // Perform base Identity Password Sign-In
            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}