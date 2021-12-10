using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.Salon
{
    public class SalonUpdateDto
    {
        [Required(ErrorMessage = "Debe ingresar una denominacion")]
        [MaxLength(250)]
        public string Denominacion { get; set; }
        [Required(ErrorMessage = "Debe ingresar un numero de  salon")]
        public int numero { get; set; }
    }
}
