using Asp.Versioning;
using Auth.Configuration;
using Auth.Data;
using Auth.Models;
using Auth.Options;
using Auth.Services;
using Auth.Services.Identity;
using Auth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging();
});


// 2. Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 3. Register Auth Services (Dependency Injection)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<ITenantService, TenantService>();

builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.SectionName));
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.Configure<SmsOptions>(builder.Configuration.GetSection(SmsOptions.SectionName));
builder.Services.AddScoped<ISmsSender, SmsSender>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

builder.Services.AddAuthorization(options =>
{
    // 1. Strict Admin policy — only users with the "Admin" role
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    // 2. Manager-specific policy — only users with the "Manager" role
    options.AddPolicy("ManagerOnly", policy =>
        policy.RequireRole("Manager"));

    // 3. Shared elevated-privilege policy — Admin OR Manager
    options.AddPolicy("ManagerOrAdmin", policy =>
        policy.RequireRole("Admin", "Manager"));

    // 4. General authenticated-user policy — any user with the "User" role
    options.AddPolicy("UserAccess", policy =>
        policy.RequireRole("User"));
});

// 4. Register Identity with ApplicationUser and ApplicationRole    
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>   
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.User.RequireUniqueEmail = true; 

    options.Tokens.ChangePhoneNumberTokenProvider = TokenOptions.DefaultPhoneProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddUserManager<ApplicationUserManager>()
.AddSignInManager<ApplicationSignInManager>()
.AddRoleManager<ApplicationRoleManager>() 
.AddDefaultTokenProviders();

// Replace built-in global UserValidator with Tenant-scoped UserValidator
builder.Services.RemoveAll<IUserValidator<ApplicationUser>>();
builder.Services.AddScoped<IUserValidator<ApplicationUser>, TenantUserValidator>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); 

}).AddMvc();
builder.Services.AddApiVersioning();
// 5. Bind JwtOptions from appsettings.json
var jwtOptions = new JwtOptions();
builder.Configuration.GetSection(JwtOptions.SectionName).Bind(jwtOptions);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));

builder.Services.AddAuthentication(options =>
{
    // Default schemes for your API's local JWTs
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
        ClockSkew = TimeSpan.Zero // Removes default 5-minute buffer delay
    };
})

.AddGoogle(options =>
{
    options.SignInScheme = IdentityConstants.ExternalScheme;
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;

    // Correlation cookie settings for local development
    options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

var app = builder.Build();

// 7. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();