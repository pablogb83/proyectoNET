using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.Eventos
{
    public class EventoUpdateDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public DateTime FechaInicioEvt { get; set; }
        public DateTime FechaFinEvt { get; set; }

        public string PhotoFileName { get; set; }
    }
}
