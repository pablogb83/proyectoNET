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
        [Required]
        [MaxLength(250)]
        public string Denominacion { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public int idEdificio { get; set; }
    }
}