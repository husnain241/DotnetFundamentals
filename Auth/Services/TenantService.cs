using Auth.Controllers;
using Auth.Data;
using Auth.DTOs.Tenant;
using Auth.Models;
using Auth.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class TenantService : ITenantService
    {
        private readonly ApplicationDbContext _context;

        public TenantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TenantDto> CreateTenantAsync(CreateTenantDto dto)
        {
            // 1. Validation Check: Empty/Whitespace Name
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Tenant name is required.");
            }

            var trimmedName = dto.Name.Trim();

            // 2. Duplication Check: Check if tenant with same name already exists (Case-Insensitive)
            bool exists = await _context.Tenants
                .AnyAsync(t => t.Name.ToLower() == trimmedName.ToLower());

            if (exists)
            {
                throw new InvalidOperationException($"Tenant with name '{trimmedName}' already exists.");
            }

            // 3. Entity Mapping & Creation
            var tenant = new Tenant
            {
                Id = Guid.NewGuid().ToString(),
                Name = trimmedName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            // 4. Return DTO
            return new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                IsActive = tenant.IsActive,
                CreatedAt = tenant.CreatedAt
            };
        }

        public async Task<IEnumerable<TenantDto>> GetAllTenantsAsync()
        {
            return await _context.Tenants
                .AsNoTracking()
                .Select(t => new TenantDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    IsActive = t.IsActive,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
        }
    }
}