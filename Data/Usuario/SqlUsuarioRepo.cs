using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoNET.Models;

namespace ProyectoNET.Data
{
    public class SqlUsuarioRepo : IUsuarioRepo
    {
        private readonly CommanderContext _context;

        public SqlUsuarioRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateUsuario(Usuario usr)
        {
             if(usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            
            _context.Usuarios.Add(usr);
        }

        public void DeleteUsuario(Usuario usr)
        {
            if(usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            _context.Usuarios.Remove(usr);
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetUsuarioById(int Id)
        {
             return _context.Usuarios.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0 );
        }

        public void UpdateUsuario(Usuario usr)
        {
            //Nothing
        }
    }
}