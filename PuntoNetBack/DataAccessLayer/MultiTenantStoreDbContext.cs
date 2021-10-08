// Copyright Finbuckle LLC, Andrew White, and Contributors.

using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{

    public class MultiTenantStoreDbContext : EFCoreStoreDbContext<TenantInfo>
    { 
        public MultiTenantStoreDbContext(DbContextOptions<MultiTenantStoreDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-30BQ617\\SQLEXPRESS;Database=NetApi2;Trusted_Connection=True;MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}