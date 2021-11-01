using BusinessLayer.IBL;
using DataAccessLayer;
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

        public bool CreateUsuarioEdificio(int usuarioId, int edificioId)
        {
            var usuario = _dalusuario.GetUsuarioById(usuarioId);
            var edificio = _daledificio.GetEdificioById(edificioId);
            if(usuario.Role!=null && (usuario.Role.NombreRol=="GESTOR" || usuario.Role.NombreRol == "PORTERO"))
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

        public IEnumerable<UsuarioEdificio> GetAllUsuarioEdificio()
        {
            return _dal.GetAllUsuarioEdificio();
        }

        public IEnumerable<Usuario> GetUsuariosEdificio(int idEdificio)
        {
            List<Usuario> usuarios = new List<Usuario>();
            var usuariosEdificio = _dal.GetUsuariosEdificio(idEdificio);
            foreach (var item in usuariosEdificio)
            {
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
