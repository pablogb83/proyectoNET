using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_UsuarioPuerta
    {
        bool SaveChanges();
        Task<bool> CreateUsuarioPuertaAsync(int usuarioId, int puertaId);
        Task<IEnumerable<UsuarioPuerta>> GetAllUsuarioPuertaAsync();
        Task<Usuario> GetUsuarioPuerta(int idPuerta);
        Puerta GetPuertaUsuario(int idUsuario);
        void DeleteUsuarioPuerta(int idUsuario);
    }
}
