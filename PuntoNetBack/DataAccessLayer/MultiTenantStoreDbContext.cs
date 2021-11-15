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

        public virtual DbSet<Producto> Productos { get; set; }

        public virtual DbSet<Suscripcion> Suscripciones { get; set; }

        public virtual DbSet<Precio> Precio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-PBMLVKPJ\\SQLEXPRESS;Database=NetApi2;Trusted_Connection=True;MultipleActiveResultSets=True");
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