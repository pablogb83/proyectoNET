using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.PuertaAccesos
{
    public class PuertaUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Denominacion { get; set; }
    }
}
