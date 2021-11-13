using DataAccessLayer.Dtos.Persona;
using DataAccessLayer.Dtos.PuertaAccesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Accesos
{
    public class AccesoReadDto
    {
      
        public int Id { get; set; }
      
        public DateTime FechaHora { get; set; }
       
        public PuertaReadDto Puerta { get; set; }
    
        public PersonaReadDto Persona { get; set; }
    }
}
