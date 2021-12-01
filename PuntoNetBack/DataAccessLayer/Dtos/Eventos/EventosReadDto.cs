using DataAccessLayer.Dtos.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Eventos
{
    public class EventosReadDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicioEvt { get; set; }
        public DateTime FechaFinEvt { get; set; }
        public SalonReadDto Salon { get; set; }

    }
}
