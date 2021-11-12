using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enum;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    [Index(nameof(nro_doc), IsUnique = true)]
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

        public string PhotoFileName { get; set; }

        public virtual ICollection<Acceso> Accesos { get; set; } = new List<Acceso>();

    }
}
