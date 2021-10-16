using Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModeloDeDominio
{
    public class Producto
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public ProductNameEnum Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public List<Precio> Precios { get; set; }
        public List<Suscripcion> Suscripciones { get; set; }
    }
}
