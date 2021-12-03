using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_UsuarioEdificio
    {
        bool SaveChanges();

        void CreateUsuarioEdificio(UsuarioEdificio usuarioEdificio);
        IEnumerable<UsuarioEdificio> GetAllUsuarioEdificio();
        IEnumerable<UsuarioEdificio> GetUsuariosEdificio(int idEdificio);
        Edificio GetEdificioUsuario(Usuario usr);
        void DeleteUsuarioEdificio(int idUsuario);
    }
}
