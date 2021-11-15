using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    public class Edificio
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



        public virtual ICollection<Salon> Salones { get; set; } = new List<Salon>();
        public virtual ICollection<Puerta> puerta_accesos { get; set; } = new List<Puerta>();

        public static implicit operator string(Edificio v)
        {
            throw new NotImplementedException();
        }
    }
}