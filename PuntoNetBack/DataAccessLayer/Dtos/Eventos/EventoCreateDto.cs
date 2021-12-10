using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.Eventos
{
    public class EventoCreateDto
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime FechaInicioEvt { get; set; }
        [Required(ErrorMessage = "La fecha de fin es requerida")]
        public DateTime FechaFinEvt { get; set; }
        public string PhotoFileName { get; set; }
        [Required(ErrorMessage ="Debe ingresar un salon")]
        public int SalonId { get; set; }
    }
}
