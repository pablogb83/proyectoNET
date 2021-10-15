using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.PuertaAccesos
{
    public class PuertaAccesoCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Denominacion { get; set; }
    }
}
