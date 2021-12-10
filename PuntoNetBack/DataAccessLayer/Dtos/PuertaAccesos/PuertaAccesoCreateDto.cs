using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.PuertaAccesos
{
    public class PuertaCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar una denominacion")]
        [MaxLength(250)]
        public string Denominacion { get; set; }
        [Required(ErrorMessage = "Debe ingresar un edificio asociado")]
        public int idEdificio { get; set; }
    }
}
