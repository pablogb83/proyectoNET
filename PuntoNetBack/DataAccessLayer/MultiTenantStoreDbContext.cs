// Copyright Finbuckle LLC, Andrew White, and Contributors.

using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;

namespace DataAccessLayer
{

    public class MultiTenantStoreDbContext : EFCoreStoreDbContext<Institucion>
    { 
        public MultiTenantStoreDbContext(DbContextOptions<MultiTenantStoreDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Institucion> Instituciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-30BQ617\\SQLEXPRESS;Database=NetApi2;Trusted_Connection=True;MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institucion>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Institucion>().Property(e => e.Identifier).ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);
        }
    }
}