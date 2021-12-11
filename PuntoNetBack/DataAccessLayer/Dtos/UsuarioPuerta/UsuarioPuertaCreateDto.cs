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
        [Required(ErrorMessage = "Debe ingresar un usuario")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "Debe ingresar una puerta")]
        public int PuertaId { get; set; }
    }
}
