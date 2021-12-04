using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Eventos
{
    public class EventoRecurrenteCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public DateTime FechaInicioEvt { get; set; }
        [Required]
        public DateTime FechaFinEvt { get; set; }
        [Required]
        public TimeSpan HoraInicio { get; set; }
        public string PhotoFileName { get; set; }
        [Required]
        public int Duracion { get; set; }
        [Required]
        public int[] Dias { get; set; }
        public int SalonId { get; set; }
    }
}
