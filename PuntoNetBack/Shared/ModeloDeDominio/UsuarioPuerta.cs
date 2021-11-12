using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    [Index(nameof(UsuarioId), IsUnique = true)]
    public class UsuarioPuerta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int PuertaId { get; set; }

        public virtual Usuario usuario { get; set; }

        public virtual Puerta puerta { get; set; }
    }
}
