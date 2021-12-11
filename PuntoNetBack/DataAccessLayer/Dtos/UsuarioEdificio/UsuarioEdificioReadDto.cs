using DataAccessLayer.Dtos.Edificios;
using DataAccessLayer.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.UsuarioEdificio
{
    public class UsuarioEdificioReadDto
    {

        [Required(ErrorMessage = "Debe ingresar un usuario")]
        public UsuarioReadDto Usuario { get; set; }
        [Required(ErrorMessage = "Debe ingresar un edificio")]
        public EdificiosReadDto Edificio { get; set; }
    }
}
