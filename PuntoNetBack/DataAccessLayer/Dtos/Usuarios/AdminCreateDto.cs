using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Usuarios
{
    public class AdminCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar el campo Email")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar el campo Password")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])).+$", ErrorMessage = "La contraseña debe tener 8 caracteres incluyendo: mayusculas, minusculas, numeros y simbolos")]
        public string PasswordPlano { get; set; }
        [Required(ErrorMessage = "Debe ingresar una institucions")]
        public string TenantId { get; set; }
    }
}
