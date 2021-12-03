using Finbuckle.MultiTenant;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.ModeloDeDominio
{
    [Table("Institucion")]
    public class Institucion : ITenantInfo
    {
        [Key]
        public string Id { get; set; }
        public string Identifier { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        //public Suscripcion Suscripcion { get; set; }
        public bool Activa { get; set; }
        public Suscripcion Suscripcion { get; set; }
        //public int UsuarioId { get; set; }
    }
}
