using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.UsuarioPuerta
{
    public class UsuarioPuertaCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int PuertaId { get; set; }
    }
}
