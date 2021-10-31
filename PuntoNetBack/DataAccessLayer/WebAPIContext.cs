﻿using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class WebAPIContext : MultiTenantDbContext
    {
        public WebAPIContext(ITenantInfo tenantInfo, DbContextOptions options) : base(tenantInfo, options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Suscripcion> Suscripcion { get; set; }
        public virtual DbSet<Precio> Precio { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Puerta> Puertas { get; set; }
        public virtual DbSet<Edificio> Edificios { get; set; }
        public virtual DbSet<Salon> Salones { get; set; }
        public virtual DbSet<UsuarioEdificio> UsuariosEdificio { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<UsuarioEdificio>()
        //    //     .HasKey(c => new { c.UsuarioId, c.EdificioId });

        //    //modelBuilder.Entity<UsuarioEdificio>()
        //    //     .HasIndex(c => c.UsuarioId).IsUnique();
        //}

    }
}
