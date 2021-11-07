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
        Task<IEnumerable<UsuarioEdificio>> GetAllUsuarioEdificio();
        Task<IEnumerable<Usuario>> GetUsuariosEdificio(int idEdificio);
        Task<Edificio> GetEdificioUsuario(int idUsuario);
        void DeleteUsuarioEdificio(int idUsuario);
    }
}
