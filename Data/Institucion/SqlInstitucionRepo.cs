using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoNET.Models;

namespace ProyectoNET.Data
{
    public class SqlInstitucionRepo : IInstitucionRepo
    {
        private readonly CommanderContext _context;

        public SqlInstitucionRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateInstitucion(Institucion inst)
        {
             if(inst == null)
            {
                throw new ArgumentNullException(nameof(inst));
            }
            
            _context.Instituciones.Add(inst);
        }

        public void DeleteInstitucion(Institucion inst)
        {
            if(inst == null)
            {
                throw new ArgumentNullException(nameof(inst));
            }
            _context.Instituciones.Remove(inst);
        }

        public IEnumerable<Institucion> GetAllInstituciones()
        {
            return _context.Instituciones.ToList();
        }

        public Institucion GetInstitucionById(int Id)
        {
             return _context.Instituciones.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0 );
        }

        public void UpdateInstitucion(Institucion inst)
        {
            //Nothing
        }
    }
}