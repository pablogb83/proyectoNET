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
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime FechaInicioEvt { get; set; }
        [Required(ErrorMessage = "La hora de inicio requerida")]
        public DateTime FechaFinEvt { get; set; }
        public int SalonId { get; set; }
    }
}
