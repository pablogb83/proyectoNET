using BusinessLayer.IBL;
using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_UsuarioPuerta : IBL_UsuarioPuerta
    {
        private readonly IDAL_UsuarioPuerta _dal;
        private readonly IDAL_Usuario _dalusuario;
        private readonly IDAL_Puerta _dalpuerta;
        private readonly IDAL_UsuarioEdificio _dalUsrEdi;

        public BL_UsuarioPuerta(IDAL_UsuarioPuerta dal, IDAL_Usuario dalusuario, IDAL_Puerta dalpuerta, IDAL_UsuarioEdificio dalUsrEdi)
        {
            _dal = dal;
            _dalusuario = dalusuario;
            _dalpuerta = dalpuerta;
            _dalUsrEdi = dalUsrEdi;

        }
        public async Task CreateUsuarioPuertaAsync(int usuarioId, int puertaId)
        {
            var usuario2 = _dal.GetUsuarioPuerta(puertaId);
            if (usuario2 != null)
            {
                if(usuario2.Id == usuarioId)
                {
                    throw new InvalidOperationException("Ya esta asignado a esa puerta");
                }
                else
                {
                    throw new InvalidOperationException("La puerta ya tiene un usuario asignado");
                }
                
            }
            var usuario = await _dalusuario.GetUsuarioByIdAsync(usuarioId);
            var puerta = _dalpuerta.GetPuertaById(puertaId);
            var edificio = _dalUsrEdi.GetEdificioUsuario(usuario);
            if (usuario.Role != null && usuario.Role == "PORTERO" && edificio !=null)
            {
                var usuarioPuerta = new UsuarioPuerta();
                usuarioPuerta.puerta = puerta;
                usuarioPuerta.usuario = usuario;
                _dal.CreateUsuarioPuerta(usuarioPuerta);
                //return true;
            }
            else
            {
                throw new InvalidOperationException("No puede seleccionar esa puerta");
            }
        }

        public void DeleteUsuarioPuerta(int idUsuario)
        {
            _dal.DeleteUsuarioPuerta(idUsuario);
        }

        public async Task<IEnumerable<UsuarioPuerta>> GetAllUsuarioPuertaAsync()
        {
            var usuariosPuerta = _dal.GetAllUsuarioPuerta();
            foreach (var item in usuariosPuerta)
            {
                item.usuario.Role = await _dalusuario.GetRolUsuario(item.usuario);
            }
            return usuariosPuerta;
            //return _dal.GetAllUsuarioPuerta();
        }

        public Puerta GetPuertaUsuario(int idUsuario)
        {
            var puerta = _dal.GetPuertaUsuario(idUsuario);
            return puerta;
        }

        public async Task<Usuario> GetUsuarioPuerta(int idPuerta)
        {
          
            var usuario = _dal.GetUsuarioPuerta(idPuerta);
            usuario.Role = await _dalusuario.GetRolUsuario(usuario);
            return usuario;

        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }
    }
}
