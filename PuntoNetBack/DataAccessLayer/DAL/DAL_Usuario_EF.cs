using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Usuario_EF : IDAL_Usuario
    {
        private readonly WebAPIContext _context;
        private readonly UserManager<Usuario> _userManager;

        public DAL_Usuario_EF(WebAPIContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            // var u1 = _context.Usuarios.Add(usr);
            //_context.SaveChanges();
            _context.TenantMismatchMode = Finbuckle.MultiTenant.TenantMismatchMode.Ignore;
            var result = await _userManager.CreateAsync(usr, password);
            var createdUser = _userManager.Users.IgnoreQueryFilters().SingleOrDefault(x => x.Email == usr.Email);
            await _userManager.AddToRoleAsync(createdUser, "ADMIN");
        }

        public void DeleteUsuario(Usuario usr)
        {
            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            _context.Usuarios.Remove(usr);
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
            //nothing
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
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
            var Porteros = await _userManager.GetUsersInRoleAsync("PORTERO");
            var Usuarios = Porteros.Concat(await _userManager.GetUsersInRoleAsync("GESTOR"));
            return await AssignRoles(Usuarios);
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

    }
}
