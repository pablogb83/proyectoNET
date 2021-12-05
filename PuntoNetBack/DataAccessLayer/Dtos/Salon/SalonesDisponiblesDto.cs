using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Salon
{
    public class SalonesDisponiblesDto
    {
        public DateTime FechaInicioEvt { get; set; }

        public DateTime FechaFinEvt { get; set; }
 
        public int EdificioId { get; set; }

        public string TipoEvento { get; set; }

        public int[] dias { get; set; }

        public int Duracion { get; set; }

        public TimeSpan HoraInicio { get; set; }
    }
}
