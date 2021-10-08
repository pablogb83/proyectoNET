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

        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int Id);
        void CreateUsuario(Usuario usr, string password);
        void UpdateUsuario(Usuario usr, string password = null);
        void DeleteUsuario(Usuario usr);
        Usuario Autenticar(string email, string password);
    }
}
