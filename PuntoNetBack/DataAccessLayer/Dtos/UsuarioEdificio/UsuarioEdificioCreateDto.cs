using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.UsuarioEdificio
{
    public class UsuarioEdificioCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar un usuario")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "Debe ingresar un edificio")]
        public int EdificioId { get; set; }
    }
}
