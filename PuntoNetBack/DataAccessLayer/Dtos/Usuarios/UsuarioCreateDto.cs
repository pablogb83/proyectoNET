using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Usuarios
{
    public class UsuarioCreateDto
    {

        [Required(ErrorMessage = "Debe ingresar un email")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])).+$", ErrorMessage = "La contraseña debe tener 8 caracteres incluyendo: mayusculas, minusculas, numeros y simbolos")]
        public string PasswordPlano { get; set; }

    }
}
