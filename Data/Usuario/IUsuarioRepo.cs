using System.Collections.Generic;
using ProyectoNET.Models;

namespace ProyectoNET.Data
{
    public interface IUsuarioRepo
    {
        bool SaveChanges();

        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int Id);
        void CreateUsuario(Usuario usr);
        void UpdateUsuario(Usuario usr);
        void DeleteUsuario(Usuario usr);

    }
}