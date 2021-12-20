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
    public class Salon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Denominacion { get; set; }
        [Required]
        public int Numero { get; set; }

        public virtual Edificio edificio { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
