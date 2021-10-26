﻿using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Institucion_EF : IDAL_Institucion
    {
        private readonly MultiTenantStoreDbContext _context;

        public DAL_Institucion_EF(MultiTenantStoreDbContext context)
        {
            _context = context;
        }

        public void CreateInstitucion(Institucion inst)
        {
            if (inst == null)
            {
                throw new ArgumentNullException(nameof(inst));
            }
            _context.Instituciones.Add(inst);
        }

        public void DeleteInstitucion(Institucion inst)
        {
            if (inst == null)
            {
                throw new ArgumentNullException(nameof(inst));
            }
            _context.Instituciones.Remove(inst);
        }

        public IEnumerable<Institucion> GetAllInstituciones()
        {
            return _context.Instituciones.ToList();
        }

        public Institucion GetInstitucionById(string Id)
        {
            return _context.Instituciones.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateInstitucion(Institucion inst)
        {
            //nothing
        }
    }
}
