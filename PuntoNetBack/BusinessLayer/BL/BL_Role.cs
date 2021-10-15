using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.BL
{
    public class BL_Role : IBL_Role
    {
        private readonly DataAccessLayer.IDAL.IDAL_Role _dal;
        private readonly DataAccessLayer.IDAL.IDAL_Usuario _dalUsr;

        public BL_Role(DataAccessLayer.IDAL.IDAL_Role dal, DataAccessLayer.IDAL.IDAL_Usuario dalUSR)
        {
            _dal = dal;
            _dalUsr = dalUSR;
        }

        public void AddRoleToUser(int rolId, int userId)
        {
            var user = _dalUsr.GetUsuarioById(userId);
            var rol = _dal.GetRoleById(rolId);
            user.Rol = rol;
            _dalUsr.SaveChanges();
            //_dalUsr.UpdateUsuario(user);
        }

        public void CreateRole(Role rol)
        {
            _dal.CreateRole(rol);
        }

        public void DeleteRole(Role rol)
        {
            _dal.DeleteRole(rol);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _dal.GetAllRoles();
        }

        public Role GetRoleById(int Id)
        {
            return _dal.GetRoleById(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateRole(Role rol)
        {
            _dal.UpdateRole(rol);
        }
    }
}