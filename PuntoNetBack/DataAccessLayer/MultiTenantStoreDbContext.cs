// Copyright Finbuckle LLC, Andrew White, and Contributors.

using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.ModeloDeDominio;

namespace DataAccessLayer
{

    public class MultiTenantStoreDbContext : EFCoreStoreDbContext<Institucion>
    {
        private readonly IConfiguration Configuration;

        public MultiTenantStoreDbContext(DbContextOptions<MultiTenantStoreDbContext> options, IConfiguration config) : base(options)
        {
            Configuration = config;
        }

        public virtual DbSet<Institucion> Instituciones { get; set; }

        public virtual DbSet<Suscripcion> Suscripciones { get; set; }

        public virtual DbSet<Precio> Precio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("CommanderConnection"));
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