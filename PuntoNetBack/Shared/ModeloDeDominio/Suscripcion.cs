using Finbuckle.MultiTenant;
using Shared.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    public class Suscripcion
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime fechainicio { get; set; }
        [Required]
        public SuscriptionEnum estado { get; set; }
        [Required]
        public virtual Producto Producto { get; set; }
    }
}
