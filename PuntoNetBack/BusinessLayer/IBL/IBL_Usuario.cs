using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Usuario
    {
        bool SaveChanges();

        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<IEnumerable<Usuario>> GetUsuariosAdmin();
        Task<IEnumerable<Usuario>> GetAdminsInstitucion(string idinstitucion);
        Task<Usuario> GetUsuarioByIdAsync(int Id);
        Task<Usuario> GetAdminByIdAsync(int Id);
        Task CreateUsuarioAsync(Usuario usr, string password);
        Task CreateAdminAsync(Usuario usr, string password);
        void UpdateUsuario(Usuario usr, string password = null);
        void DeleteUsuario(Usuario usr);
        void DeleteAdmin(Usuario usr);
        Task<Usuario> Autenticar(string email, string password);
        Task<string> GetRolUsuario(Usuario user);
        public Task AddRoleToUserAsync(int rolId, int userId);



    }
}
