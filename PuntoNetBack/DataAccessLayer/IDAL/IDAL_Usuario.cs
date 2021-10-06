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
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int Id);
        void CreateUsuario(Usuario usr, string password, int idInst);
        void UpdateUsuario(Usuario usr, string password = null);
        void DeleteUsuario(Usuario usr);
        Usuario Autenticar(string email, string password);

    }
}
