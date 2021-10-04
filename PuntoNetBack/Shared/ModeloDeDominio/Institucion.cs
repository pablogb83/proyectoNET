using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModeloDeDominio
{
    public class Institucion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }

        public ICollection<Edificio> edificios { get; set; } = new List<Edificio>();
    }
}
