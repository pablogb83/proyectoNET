using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Usuario : IBL_Usuario
    {
        private readonly DataAccessLayer.IDAL.IDAL_Usuario _dal;

        public BL_Usuario(DataAccessLayer.IDAL.IDAL_Usuario dal)
        {
            _dal = dal;
        }

        public Usuario Autenticar(string email, string password)
        {
            return _dal.Autenticar(email, password);
        }

        public void CreateUsuario(Usuario usr, string password)
        {
            _dal.CreateUsuario(usr, password);
        }

        public void DeleteUsuario(Usuario usr)
        {
            _dal.DeleteUsuario(usr);
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _dal.GetAllUsuarios();
        }

        public Usuario GetUsuarioById(int Id)
        {
            return _dal.GetUsuarioById(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateUsuario(Usuario usr, string password = null)
        {
            _dal.UpdateUsuario(usr,password);
        }
    }
}
