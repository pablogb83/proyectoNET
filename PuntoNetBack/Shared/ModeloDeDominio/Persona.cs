using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shared.Enum;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public TipoDocumento tipo_doc { get; set; }
        [Required]
        public string nro_doc { get; set; }
    }
}
