using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_UsuarioEdificio
    {
        bool SaveChanges();
        Task<bool> CreateUsuarioEdificioAsync(int usuarioId, int edificioId);
        IEnumerable<UsuarioEdificio> GetAllUsuarioEdificio();
        Task<IEnumerable<Usuario>> GetUsuariosEdificio(int idEdificio);
        void DeleteUsuarioEdificio(int idUsuario);
    }
}
