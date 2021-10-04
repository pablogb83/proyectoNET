using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_PuertaAcceso_EF //: IDAL_PuertaAcceso
    {
     /*   private readonly WebAPIContext _context;

        public DAL_PuertaAcceso_EF(WebAPIContext context)
        {
            _context = context;
        }

        public void CreatePuertaAcceso(int idEdificio, PuertaAcceso puertaAcceso)
        {
           Edificio edificio =  _context.Edificio.FirstOrDefault(p => p.Id == Id);
            if (edificio == null)
            {
                throw new Exception("No se encontró ningun Edificio con ese ID");
            }

            if (_context.Usuarios.Any(x => x.Email == usr.Email))
                throw new AppException("Email " + usr.Email + " ya esta registrado");

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("El password es requerido");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            usr.Password = passwordHash;
            usr.PasswordSalt = passwordSalt;

            _context.Usuarios.Add(usr);
        }

        public void DeleteUsuario(Usuario usr)
        {
            if (usr == null)
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
        }*/
    }
}
