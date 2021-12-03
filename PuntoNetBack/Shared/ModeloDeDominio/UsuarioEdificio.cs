using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    [Index(nameof(UsuarioId), IsUnique = true)]
    public class UsuarioEdificio
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int EdificioId { get; set; }
   
        public virtual Usuario usuario { get; set; }
   
        public virtual Edificio edificio { get; set; }
    }
}
