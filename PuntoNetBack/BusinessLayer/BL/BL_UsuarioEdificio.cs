using BusinessLayer.IBL;
using DataAccessLayer;
using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_UsuarioEdificio : IBL_UsuarioEdificio
    {
        private readonly IDAL_UsuarioEdificio _dal;
        private readonly IDAL_Usuario _dalusuario;
        private readonly IDAL_Edificio _daledificio;

        public BL_UsuarioEdificio(IDAL_UsuarioEdificio dal, IDAL_Usuario dalusuario, IDAL_Edificio daledificio)
        {
            _dal = dal;
            _dalusuario = dalusuario;
            _daledificio = daledificio;

        }

        public async Task<bool> CreateUsuarioEdificioAsync(int usuarioId, int edificioId)
        {
            var usuario = await _dalusuario.GetUsuarioByIdAsync(usuarioId);
            var edificio = _daledificio.GetEdificioById(edificioId);
            var userEdificio = await GetEdificioUsuario(usuarioId);
            if (userEdificio != null)
            {
                throw new AppException("El usuario ya tiene un edificio asignado");
            }
            if(usuario.Role!=null && (usuario.Role=="GESTOR" || usuario.Role == "PORTERO"))
            {
                var usuarioEdificio = new UsuarioEdificio();
                usuarioEdificio.edificio = edificio;
                usuarioEdificio.usuario = usuario;
                _dal.CreateUsuarioEdificio(usuarioEdificio);
                return true;
            }
            return false;
        }

        public void DeleteUsuarioEdificio(int idUsuario)
        {
            _dal.DeleteUsuarioEdificio(idUsuario);
        }

        public async Task<IEnumerable<UsuarioEdificio>> GetAllUsuarioEdificio()
        {
            //return _dal.GetAllUsuarioEdificio();
            var usuariosEdificio = _dal.GetAllUsuarioEdificio();
            foreach (var item in usuariosEdificio)
            {
                item.usuario.Role = await _dalusuario.GetRolUsuario(item.usuario);
            }
            return usuariosEdificio;

        }

        public async Task<Edificio> GetEdificioUsuario(int idUsuario)
        {
            var usuario = await _dalusuario.GetUsuarioByIdAsync(idUsuario);
            return _dal.GetEdificioUsuario(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosEdificio(int idEdificio)
        {
            List<Usuario> usuarios = new List<Usuario>();
            var usuariosEdificio = _dal.GetUsuariosEdificio(idEdificio);
            foreach (var item in usuariosEdificio)
            {
                item.usuario.Role = await _dalusuario.GetRolUsuario(item.usuario);
                usuarios.Add(item.usuario);
            }
            return usuarios;
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }
    }
}
