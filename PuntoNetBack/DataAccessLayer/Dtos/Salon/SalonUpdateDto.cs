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
        [Required]
        [MaxLength(250)]
        public string Denominacion { get; set; }
        [Required]
        public int numero { get; set; }
    }
}
