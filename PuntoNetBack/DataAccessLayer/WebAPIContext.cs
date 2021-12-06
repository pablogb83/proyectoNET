using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class WebAPIContext : MultiTenantIdentityDbContext<Usuario, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public WebAPIContext(ITenantInfo tenantInfo, DbContextOptions<WebAPIContext> options) : base(tenantInfo, options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Puerta> Puertas { get; set; }
        public virtual DbSet<Edificio> Edificios { get; set; }
        public virtual DbSet<Salon> Salones { get; set; }
        public virtual DbSet<UsuarioEdificio> UsuariosEdificio { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<UsuarioPuerta> UsuarioPuerta { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Acceso> Accesos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(pk => new { pk.UserId, pk.RoleId });
            TenantMismatchMode = TenantMismatchMode.Ignore;
        }



    }
}
