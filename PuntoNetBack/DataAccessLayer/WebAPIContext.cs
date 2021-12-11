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
        private const string defaultTenantId = "240ed6a6-e391-4745-9278-ff5e66189583";
        private const int adminId = 1;
        private const int roleId = 1;
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
        public virtual DbSet<Noticias> Noticias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(pk => new { pk.UserId, pk.RoleId });
            modelBuilder.Entity<Role>().HasData(new Role { Name = "SUPERADMIN", NormalizedName = "SUPERADMIN", ConcurrencyStamp = new Guid().ToString(), Id = roleId });
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                PhoneNumber = "098549382",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D"),
                ConcurrencyStamp = new Guid().ToString("D"),
                TwoFactorEnabled = false,
                LockoutEnd = new DateTime(),
                LockoutEnabled = true,
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAEAACcQAAAAEDScDguK3Sx4oLf+Zh+A8etz8lIrm2TH0yCx10v7asCjqErAXyq2gOad712ILTfrKg==",
                TenantId = defaultTenantId
            }); ;
            modelBuilder.Entity<UserRole>().HasData(new UserRole { UserId=adminId,RoleId= roleId });
            TenantMismatchMode = TenantMismatchMode.Ignore;
        }



    }
}
