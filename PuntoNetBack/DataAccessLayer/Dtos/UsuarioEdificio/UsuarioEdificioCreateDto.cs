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
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int EdificioId { get; set; }
    }
}
