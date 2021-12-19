using DataAccessLayer.IDAL;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataAccessLayer.DAL
{
    public class DAL_Acceso_EF : IDAL_Acceso
    {
        private readonly WebAPIContext _context;

        public DAL_Acceso_EF(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateAcceso(Acceso acc)
        {
            if (acc == null)
            {
                throw new ArgumentNullException(nameof(acc));
            }
            _context.Accesos.Add(acc);
        }

        public void DeleteAcceso(Acceso acc)
        {
            if (acc == null)
            {
                throw new ArgumentNullException(nameof(acc));
            }
            _context.Accesos.Remove(acc);
        }

        public Acceso GetAccesoById(int Id)
        {
            return _context.Accesos.FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Acceso> GetAccesosEdificio(int idEdificio)
        {
            List<Acceso> accesos = new List<Acceso>();
            Edificio edi = _context.Edificios.FirstOrDefault(p => p.Id == idEdificio);
            if( edi!=null)
            {
                if (edi.puerta_accesos != null)
                {
                    foreach (var puerta in edi.puerta_accesos)
                    {
                        //List<Acceso> accesosPuerta = (List<Acceso>)puerta.Accesos;
                        accesos.AddRange(puerta.Accesos);
                    }
                }
            }
            return accesos.Where(x => x.TenantId == _context.TenantInfo.Id); ;
        }

        public IEnumerable<Acceso> GetAccesosPersona(int idPersona)
        {
            Persona prs = _context.Personas.IgnoreQueryFilters().FirstOrDefault(p => p.Id == idPersona);
            if (prs != null)
            {
                return prs.Accesos.Where(x => x.TenantId == _context.TenantInfo.Id); ;
            }
            return null;
        }

        public IEnumerable<Acceso> GetAccesosPuerta(int idPuerta)
        {
            Puerta pta = _context.Puertas.IgnoreQueryFilters().FirstOrDefault(p => p.Id == idPuerta);
            if (pta != null)
            {
                var accesos = _context.Accesos.IgnoreQueryFilters().Include(i => i.Persona).Where(acc => acc.Puerta.Id == idPuerta).ToList();
                return pta.Accesos.Where(x => x.TenantId == _context.TenantInfo.Id);
            }
            return null;
        }

        public IEnumerable<Acceso> GetAllAccesos()
        {
            var accesos = _context.Accesos.IgnoreQueryFilters().Include(i => i.Persona).ToList();
            Debug.WriteLine(_context.TenantInfo);
            return accesos.Where(x=>x.TenantId==_context.TenantInfo.Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateAcceso(Acceso acc)
        {
            //nothing
        }
    }
}
