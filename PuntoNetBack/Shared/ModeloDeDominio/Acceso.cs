using Finbuckle.MultiTenant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    public class Acceso
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime FechaHora { get; set; }
        [Required]
        public virtual Puerta Puerta { get; set; }
        [Required]
        public virtual Persona Persona { get; set; }
        public string TenantId { get; set; }
    }
}
