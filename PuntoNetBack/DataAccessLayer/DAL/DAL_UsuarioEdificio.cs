using DataAccessLayer.IDAL;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_UsuarioEdificio : IDAL_UsuarioEdificio
    {
        private readonly WebAPIContext _context;

        public DAL_UsuarioEdificio(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateUsuarioEdificio(UsuarioEdificio usuarioEdificio)
        {
            _context.UsuariosEdificio.Add(usuarioEdificio);
        }

        public void DeleteUsuarioEdificio(int idUsuario)
        {

            var usarioEdificio = _context.UsuariosEdificio.Single(u => u.UsuarioId == idUsuario);
            _context.Remove(usarioEdificio);
      

            //    .Where(u => u.UsuarioId == idUsuario)
            //_context.Remove(_context.UsuariosEdificio
            //    .Where(u => u.UsuarioId == idUsuario));

        }

        public IEnumerable<UsuarioEdificio> GetAllUsuarioEdificio()
        {
            return _context.UsuariosEdificio.ToList();
        }

        public Edificio GetEdificioUsuario(Usuario usr)
        {
            var usuarioEdificio = _context.UsuariosEdificio.Where(u => u.usuario == usr).FirstOrDefault();
            if (usuarioEdificio != null)
            {
                return usuarioEdificio.edificio;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UsuarioEdificio> GetUsuariosEdificio(int idEdificio)
        {
            return _context.UsuariosEdificio
                .Where(u => u.EdificioId == idEdificio).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
