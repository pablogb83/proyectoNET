using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_UsuarioPuerta
    {
        bool SaveChanges();

        void CreateUsuarioPuerta(UsuarioPuerta usuarioPuerta);
        IEnumerable<UsuarioPuerta> GetAllUsuarioPuerta();
        Usuario GetUsuarioPuerta(int idPuerta);
        void DeleteUsuarioPuerta(int idUsuario);
    }
}
