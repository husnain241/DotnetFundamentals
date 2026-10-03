using Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Auth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Required to map default Identity tables

            builder.Entity<Product>()
                .HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");

            builder.Entity<Product>()
                .HasIndex(p => p.Price)
                .HasDatabaseName("IX_Products_Price");

            builder.Entity<ApplicationUser>(b =>
            {
                b.HasOne(u => u.Tenant)
                 .WithMany()
                 .HasForeignKey(u => u.TenantId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(u => u.TenantId)
                 .HasDatabaseName("IX_AspNetUsers_TenantId");
            });
        }
    }
}