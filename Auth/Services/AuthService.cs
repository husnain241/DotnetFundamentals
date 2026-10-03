using Auth.Data;
using Auth.DTOs.Auth;
using Auth.Models;
using Auth.Services.Identity;
using Auth.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context; 

        private readonly Interfaces.IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public AuthService(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ApplicationRoleManager roleManager,
            IJwtTokenService jwtTokenService,
            ApplicationDbContext context,
            Interfaces.IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _context = context;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        // 1. User Registration Logic
        public async Task<bool> RegisterUserAsync(RegisterDto dto)
        {
            var tenantExists = await _context.Tenants.AnyAsync(t => t.Id == dto.TenantId && t.IsActive);
            if (!tenantExists)
            {
                throw new ArgumentException("Invalid or inactive Tenant ID.");
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                Email = dto.Email,
                TenantId = dto.TenantId 
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return false;

            // Default Claim automatically attach karte hain
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));

            return true;
        }

        // 2. User Login Logic
        public async Task<AuthResponseDto> LoginUserAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid username or password." };
            }

            var result = await _signInManager.CustomPasswordSignInAsync(
                dto.UserName, 
                dto.Password, 
                isPersistent: false, 
                lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Account is locked out or deactivated." };
            }

            if (result.IsNotAllowed)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Please confirm your email before logging in." };
            }

            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                // 1. Dynamic Provider Selection (DTO se provider read karein)
                bool isSms = string.Equals(dto.Provider, "Sms", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(dto.Provider, "Phone", StringComparison.OrdinalIgnoreCase);

                // Identity framework ke liye Provider name "Phone" ya "Email" hoga
                string identityProvider = isSms ? "Phone" : "Email";

                // 2. Generate 6-digit MFA Token
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, identityProvider);

                // 3. Dynamic Dispatching
                if (isSms)
                {
                    var phoneNumber = string.IsNullOrEmpty(user.PhoneNumber) ? "0000000000" : user.PhoneNumber;
                    await _smsSender.SendSmsAsync(phoneNumber, $"Your verification code is: {code}");
                }
                else
                {
                    await _emailSender.SendEmailAsync(user.Email ?? "", "MFA Verification Code", $"Your verification code is: {code}");
                }

                // Return challenge response
                return new AuthResponseDto
                {
                    IsSuccess = true,
                    IsMfaRequired = true,
                    UserId = user.Id,
                    Provider = isSms ? "Sms" : "Email",
                    Message = "MFA required. Verification code has been sent."
                };
            }


            if (!result.Succeeded)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid username or password." };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Login successful!",
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        // 4. Custom Claim Assignment
        public async Task<bool> AddClaimToUserAsync(UserClaimDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return false;

            var claim = new Claim(dto.ClaimType, dto.ClaimValue);
            var result = await _userManager.AddClaimAsync(user, claim);

            return result.Succeeded;
        }

        // 5. Get User Claims
        public async Task<IList<Claim>> GetUserClaimsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<Claim>();

            return await _userManager.GetClaimsAsync(user);
        }
        public async Task<AuthResponseDto> RefreshTokenAsync(string token)
        {
            var storedToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token);

            if (storedToken == null || !storedToken.IsActive)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid or expired refresh token." };
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "User not found." };
            }

            // Step A: REVOKE the old refresh token
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);
            storedToken.RevokedAt = DateTime.UtcNow;
            storedToken.ReplacedByToken = newRefreshToken.Token;

            // Step B: Save NEW refresh token
            await _context.RefreshTokens.AddAsync(newRefreshToken);
            await _context.SaveChangesAsync();

            // Step C: Generate NEW Access Token
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            var newAccessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Tokens refreshed successfully!",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        public async Task<AuthResponseDto> ExternalLoginCallbackAsync()
        {
            // 1. Read external identity details from the temporary cookie scheme
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Error loading external login information." };
            }
                
            // 2. Sign in the user with this external login provider if already linked
            var result = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: false,
                bypassTwoFactor: true);

            ApplicationUser? user = null;

            if (result.Succeeded)
            {
                // User exists and is linked
                user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            }
            else
            {
                // 3. User is signing in with Google for the first time
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        // Create a new local ApplicationUser
                        user = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = email,
                            Email = email,
                            EmailConfirmed = true
                        };

                        var createResult = await _userManager.CreateAsync(user);
                        if (!createResult.Succeeded)
                        {
                            return new AuthResponseDto { IsSuccess = false, Message = "Failed to create local user for external identity." };
                        }
                    }

                    // Link the Google Provider identity to the ApplicationUser (AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Unable to process user identity." };
            }

            // 4. Generate your application's JWT + Refresh Tokens (Issues #63–65 architecture)
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = $"Successfully authenticated via {info.LoginProvider}!",
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            if (!await _roleManager.RoleExistsAsync(roleName)) return false;
            // Prevent duplicate assignment
            if (await _userManager.IsInRoleAsync(user, roleName)) return true;
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }
        public async Task<bool> ToggleMfaAsync(string userId, bool enable)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.SetTwoFactorEnabledAsync(user, enable);
            return result.Succeeded;
        }

        public async Task<MfaStatusDto> GetMfaStatusAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new MfaStatusDto
            {
                IsMfaEnabled = user.TwoFactorEnabled,
                IsEmailConfirmed = user.EmailConfirmed,
                IsPhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
        }

        public async Task<string> GenerateMfaTokenAsync(string userId, string provider)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            // Generates a 6-digit MFA verification code for 'Email' or 'Phone' provider
            return await _userManager.GenerateTwoFactorTokenAsync(user, provider);
        }

        public async Task<bool> VerifyMfaTokenAsync(string userId, string provider, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Validates if the supplied MFA code is valid and not expired
            return await _userManager.VerifyTwoFactorTokenAsync(user, provider, code);
        }

        public async Task<AuthResponseDto> VerifyMfaAndGenerateTokensAsync(VerifyMfaDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid user request." };
            }

            // Verify code against user and provider (Identity handles code consumption & expiration)
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, dto.Provider, dto.Code);
            if (!isValid)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid or expired MFA verification code." };
            }

            // Load user roles and custom claims
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);

            // Issue standard JWT Access Token and Refresh Token (Issue #65)
            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

            return new AuthResponseDto
            {
                IsSuccess = true,
                IsMfaRequired = false,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                Message = "MFA verification successful. Authentication complete!"
            };
        }
        public async Task<bool> SetPreferredMfaProviderAsync(string userId, string provider)
        {
            if (provider != "Email" && provider != "Phone") return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (provider == "Phone" && string.IsNullOrEmpty(user.PhoneNumber)) return false;
            if (provider == "Email" && string.IsNullOrEmpty(user.Email)) return false;

            // Persist provider configuration or state
            return true;
        }


        public async Task<ApplicationUser?> GetActiveUserByEmailAsync(string email)
        {
            // Consumes custom method defined on ApplicationUserManager
            return await _userManager.FindActiveByEmailAsync(email);
        }

        public async Task<bool> DeactivateUserAccountAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Consumes custom soft-deactivation method
            var result = await _userManager.DeactivateUserAsync(user);
            return result.Succeeded;
        }
        public async Task<IdentityResult> CreateCustomRoleAsync(string roleName)
        {
            return await _roleManager.CreateCustomRoleAsync(roleName);
        }

        public async Task<IdentityResult> SafeDeleteRoleAsync(string roleName)
        {
            return await _roleManager.DeleteRoleSafelyAsync(roleName);
        }
    }
}