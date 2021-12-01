using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaInicioEvt { get; set; }

        [Required]
        public DateTime FechaFinEvt { get; set; }

        public string PhotoFileName { get; set; }

    }
}
