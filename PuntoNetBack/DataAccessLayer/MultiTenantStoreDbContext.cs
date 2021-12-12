// Copyright Finbuckle LLC, Andrew White, and Contributors.

using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.ModeloDeDominio;
using System;

namespace DataAccessLayer
{

    public class MultiTenantStoreDbContext : EFCoreStoreDbContext<Institucion>
    {
        private readonly IConfiguration Configuration;
        private const string id = "240ed6a6-e391-4745-9278-ff5e66189583";


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
            modelBuilder.Entity<Institucion>().HasData(new Institucion { Id = id, Identifier = id, Name = "Puertan", Activa = false,Telefono="0999999" ,Direccion="Segurola 1234" });
            base.OnModelCreating(modelBuilder);
        }
    }
}