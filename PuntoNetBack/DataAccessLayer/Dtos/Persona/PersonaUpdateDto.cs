using Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Persona
{
    public class PersonaUpdateDto
    {

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MaxLength(250)]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Debe ingresar un telefono")]
        [RegularExpression(@"^((?=.*[0-9])).+$", ErrorMessage = "El telefono solo puede tener numeros")]
        [MaxLength(9, ErrorMessage = "El telefono puede tener un largo maximo de 9 numeros")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Debe ingresar un email")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar un tipo de documento")]
        public TipoDocumento tipo_doc { get; set; }
        [Required(ErrorMessage = "Debe ingresar un numero de documento")]
        public string nro_doc { get; set; }

        public string PhotoFileName { get; set; }
    }
}
