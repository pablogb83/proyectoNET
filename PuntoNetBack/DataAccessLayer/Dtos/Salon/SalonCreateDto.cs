using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Dtos.Salon
{
    public class SalonCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar una denominacion")]
        [MaxLength(250)]
        public string Denominacion { get; set; }
        [Required(ErrorMessage = "Debe ingresar un numero de  salon")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Debe ingresar un edificio asociado")]
        public int idEdificio { get; set; }
    }
}