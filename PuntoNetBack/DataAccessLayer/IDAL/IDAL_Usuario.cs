using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Usuario
    {
        bool SaveChanges();
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<IEnumerable<Usuario>> GetUsuariosAdmin();
        Task<IEnumerable<Usuario>> GetAdminsInstitucion(string idinstitucion);
        Task<Usuario> GetUsuarioByIdAsync(int Id);
        Task<Usuario> GetAdminByIdAsync(int Id);
        Task<string> GetRolUsuario(Usuario user);
        Task CreateUsuarioAsync(Usuario usr, string password);
        Task CreateAdminAsync(Usuario usr, string password);
        void UpdateUsuario(Usuario usr, string password = null);
        void DeleteUsuario(Usuario usr);
        void DeleteAdmin(Usuario usr);
        Task<Usuario> AutenticarAsync(string email, string password);
        public Task AddRoleToUserAsync(Usuario userId, string Role);

    }
}
