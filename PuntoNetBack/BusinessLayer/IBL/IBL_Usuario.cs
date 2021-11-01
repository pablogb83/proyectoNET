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
        Task<Usuario> GetUsuarioByIdAsync(int Id);
        Task CreateUsuarioAsync(Usuario usr, string password);
        void UpdateUsuario(Usuario usr, string password = null);
        void DeleteUsuario(Usuario usr);
        Task<Usuario> Autenticar(string email, string password);
        Task<string> GetRolUsuario(Usuario user);
        public Task AddRoleToUserAsync(int rolId, int userId);

    }
}
