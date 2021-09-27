using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoNET.Models;
using ProyectoNET.Helpers;

namespace ProyectoNET.Data
{
    public class SqlUsuarioRepo : IUsuarioRepo
    {
        private readonly CommanderContext _context;

        public SqlUsuarioRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateUsuario(Usuario usr, string password)
        {
             if(usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }

            if(_context.Usuarios.Any(x=> x.Email == usr.Email))
                throw new AppException("Email " + usr.Email + " ya esta registrado");

            if(string.IsNullOrWhiteSpace(password))
                throw new AppException("El password es requerido");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            usr.Password = passwordHash;
            usr.PasswordSalt = passwordSalt;
            
            _context.Usuarios.Add(usr);
        }

        public void DeleteUsuario(Usuario usr)
        {
            if(usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            _context.Usuarios.Remove(usr);
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetUsuarioById(int Id)
        {
             return _context.Usuarios.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0 );
        }

        public void UpdateUsuario(Usuario usr, string password)
        {
            //Nothing
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

        public Usuario Autenticar(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Usuarios.SingleOrDefault(x => x.Email == email);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.Password, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }
    }
}