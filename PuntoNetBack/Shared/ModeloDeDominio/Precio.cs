using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModeloDeDominio
{
    public class Precio
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public float Valor { get; set; }
        [Required]
        public DateTime FechaVigencia { get; set; }
    }
}
