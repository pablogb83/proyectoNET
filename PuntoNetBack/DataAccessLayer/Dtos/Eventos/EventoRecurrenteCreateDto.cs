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
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime FechaInicioEvt { get; set; }
        [Required(ErrorMessage = "La fecha de fin es requerida")]
        public DateTime FechaFinEvt { get; set; }
        [Required(ErrorMessage = "La hora de inicio requerida")]
        public TimeSpan HoraInicio { get; set; }
        public string PhotoFileName { get; set; }
        [Required(ErrorMessage = "La duracion del evento es requerida")]
        public int Duracion { get; set; }
        [Required(ErrorMessage = "Los dias del evento son requerida")]
        public int[] Dias { get; set; }
        [Required(ErrorMessage = "El salon del evento es requerido")]
        public int SalonId { get; set; }
    }
}
