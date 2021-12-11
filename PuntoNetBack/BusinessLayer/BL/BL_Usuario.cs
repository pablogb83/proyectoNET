using BusinessLayer.IBL;
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
    public class BL_Usuario : IBL_Usuario
    {
        private readonly DataAccessLayer.IDAL.IDAL_Usuario _dal;
        private readonly IDAL_Role _dalRol;


        public BL_Usuario(DataAccessLayer.IDAL.IDAL_Usuario dal, IDAL_Role dalRol)
        {
            _dal = dal;
            _dalRol = dalRol;
        }

        public Task<Usuario> Autenticar(string email, string password)
        {
            return _dal.AutenticarAsync(email, password);
        }

        public async Task CreateUsuarioAsync(Usuario usr, string password)
        {
            await _dal.CreateUsuarioAsync(usr, password);
        }

        public void DeleteUsuario(Usuario usr)
        {
            _dal.DeleteUsuario(usr);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _dal.GetAllUsuariosAsync();
        }

        public Task<string> GetRolUsuario(Usuario user)
        {
            return _dal.GetRolUsuario(user);
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int Id)
        {
            return await _dal.GetUsuarioByIdAsync(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateUsuario(Usuario usr, string password = null)
        {
            _dal.UpdateUsuario(usr,password);
        }

        public async Task AddRoleToUserAsync(int rolId, int userId)
        {
            Role rol = _dalRol.GetRoleById(rolId);
            if (rol == null)
            {
                throw new KeyNotFoundException("El rol no existe");
            }
            if(rol.NormalizedName != "PORTERO" || rol.NormalizedName != "GESTOR")
            {
                throw new AppException("No puede asignar ese rol al usuario");
            }
            Usuario user = await _dal.GetUsuarioByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("El usuario no existe");
            }
            await _dal.AddRoleToUserAsync(user, rol.Name);
        }
        public async Task<IEnumerable<Usuario>> GetUsuariosAdmin()
        {
            return  await _dal.GetUsuariosAdmin();
        }

        public async Task CreateAdminAsync(Usuario usr, string password)
        {
            await _dal.CreateAdminAsync(usr, password);
        }
    }
}
