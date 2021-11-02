using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_UsuarioPuerta : IDAL_UsuarioPuerta
    {
        private readonly WebAPIContext _context;

        public DAL_UsuarioPuerta(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateUsuarioPuerta(UsuarioPuerta usuarioPuerta)
        {
            _context.UsuarioPuerta.Add(usuarioPuerta);
        }

        public void DeleteUsuarioPuerta(int idUsuario)
        {
            var usarioPuerta = _context.UsuarioPuerta.Single(u => u.UsuarioId == idUsuario);
            _context.Remove(usarioPuerta);
        }

        public IEnumerable<UsuarioPuerta> GetAllUsuarioPuerta()
        {
            return _context.UsuarioPuerta.ToList();
        }

        public Usuario GetUsuarioPuerta(int idPuerta)
        {
            return _context.UsuarioPuerta
                .Where(u => u.PuertaId == idPuerta).FirstOrDefault().usuario;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
