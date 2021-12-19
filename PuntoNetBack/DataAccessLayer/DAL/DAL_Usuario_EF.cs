using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Usuario_EF : IDAL_Usuario
    {
        private readonly WebAPIContext _context;
        private readonly MultiTenantStoreDbContext _multiTenantContext;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DAL_Usuario_EF(WebAPIContext context, UserManager<Usuario> userManager, RoleManager<Role> roleManager, MultiTenantStoreDbContext multiContext)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _multiTenantContext = multiContext;
        }

        public async Task<Usuario> AutenticarAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Usuarios.IgnoreQueryFilters().SingleOrDefault(x => x.Email == email);

            // check if username exists
            if (user == null)
                return null;

            if(await _userManager.CheckPasswordAsync(user, password))
            {
                var role = await _userManager.GetRolesAsync(user);
                return user;
            }
            return null;
        }

        public async Task CreateUsuarioAsync(Usuario usr, string password)
        {
            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            usr.UserName = usr.Email;
            if (_context.Usuarios.Any(x => x.Email == usr.Email))
                throw new AppException("Email " + usr.Email + " ya esta registrado");

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("El password es requerido");

            var result = await _userManager.CreateAsync(usr, password);
        }

        public async Task CreateAdminAsync(Usuario usr, string password)
        {
            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            usr.UserName = usr.Email;
            if (_context.Usuarios.IgnoreQueryFilters().Any(x => x.Email == usr.Email))
                throw new AppException("Email " + usr.Email + " ya esta registrado");

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("El password es requerido");
            _context.TenantMismatchMode = Finbuckle.MultiTenant.TenantMismatchMode.Ignore;
            
            var result = await _userManager.CreateAsync(usr, password);
            if (result.Succeeded)
            {
                var createdUser = _userManager.Users.IgnoreQueryFilters().SingleOrDefault(x => x.Email == usr.Email);
                await _userManager.AddToRoleAsync(createdUser, "ADMIN");
            }
            else
            {
                throw new AppException("Error en la creacion del usuario");
            }
        }

        public void DeleteUsuario(Usuario usr)
        {
            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            _context.Usuarios.Remove(usr);
        }

        public void DeleteAdmin(Usuario usr)
        {
            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            var deleteUser = _context.Usuarios.IgnoreQueryFilters().FirstOrDefault(x=>x.Id==usr.Id);
            _context.Entry(deleteUser).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            var UsuariosPrueba = _context.Usuarios;
            var Usuarios = await AssignRoles(_context.Usuarios.ToList());
            return Usuarios;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int Id)
        {
            Usuario user = _context.Usuarios.FirstOrDefault(p => p.Id == Id);
            var roles = await _userManager.GetRolesAsync(user);
            user.Role = roles.FirstOrDefault();
            return _context.Usuarios.FirstOrDefault(p => p.Id == Id);

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUsuario(Usuario usr, string password = null)
        {
        }

        public async Task<string> GetRolUsuario(Usuario user)
        {
            var role = await _userManager.GetRolesAsync(user);
            if (role == null)
            {
                return null;
            }
            return role.FirstOrDefault();
        }

        public async Task AddRoleToUserAsync(Usuario userId, string Role)
        {
            var roles = await _userManager.GetRolesAsync(userId);
            var user = await _userManager.FindByNameAsync(userId.UserName);
            if (roles.Count != 0)
            {
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
            }
            var roles2 = await _userManager.GetRolesAsync(userId);

            await _userManager.AddToRoleAsync(userId, Role);
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAdmin()
        {
            var usersWithoutAnyRole = _context.Usuarios
            .Where(c => !_context.UserRoles
            .Select(b => b.UserId).Distinct()
            .Contains(c.Id)).ToList();
            var Porteros = await _userManager.GetUsersInRoleAsync("PORTERO");
            var Usuarios = Porteros.Concat(await _userManager.GetUsersInRoleAsync("GESTOR"));
            return await AssignRoles(Usuarios.Concat(usersWithoutAnyRole));
        }

        private async Task<IEnumerable<Usuario>> AssignRoles(IEnumerable<Usuario> Usuarios)
        {
            foreach (var usr in Usuarios)
            {
                var role = await GetRolUsuario(usr);
                usr.Role = role ?? "UNDEFINED";
            }
            return Usuarios;
        }

        public async Task<IEnumerable<Usuario>> GetAdminsInstitucion(string idinstitucion)
        {
            var Usuarios = await AssignRoles(_context.Users.IgnoreQueryFilters().Where(x => x.TenantId == idinstitucion && x.LockoutEnabled));
            var usuariosFiltrados = Usuarios.Where(x => x.Role == "ADMIN");
            return usuariosFiltrados;
        }

        public async Task<Usuario> GetAdminByIdAsync(int Id)
        {
            Usuario user = _context.Usuarios.IgnoreQueryFilters().FirstOrDefault(p => p.Id == Id);
            var roles = await _userManager.GetRolesAsync(user);
            user.Role = roles.FirstOrDefault();
            _context.TenantMismatchMode = Finbuckle.MultiTenant.TenantMismatchMode.Ignore;
            return user ;
        }
    }
}
