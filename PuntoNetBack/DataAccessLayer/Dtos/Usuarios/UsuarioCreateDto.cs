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

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordPlano { get; set; }

    }
}
