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
    public class Noticias
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string PublicadoPor { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        public string PhotoFileName { get; set; }
    }
}
