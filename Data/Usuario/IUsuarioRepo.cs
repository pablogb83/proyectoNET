using System.Collections.Generic;
using ProyectoNET.Models;

namespace ProyectoNET.Data
{
    public interface IUsuarioRepo
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