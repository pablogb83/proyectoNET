using CsvHelper.Configuration.Attributes;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Persona
{
    public class PersonaCreateDto
    {

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MaxLength(250)]
        [Index(0)]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Debe ingresar un apellido")]
        [Index(1)]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Debe ingresar un telefono")]
        [RegularExpression(@"^((?=.*[0-9])).+$", ErrorMessage = "El telefono solo puede tener numeros")]
        [MaxLength(9,ErrorMessage ="El telefono puede tener un largo maximo de 9 numeros")]
        [Index(2)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Debe ingresar un email")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email valido")]
        [Index(3)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar un tipo de documento")]
        [EnumDataType(typeof(TipoDocumento),ErrorMessage = "Debe ingresar un tipo de documento valido")]
        [Index(4)]
        public TipoDocumento tipo_doc { get; set; }
        [Required(ErrorMessage = "Debe ingresar un numero de documento")]
        [MaxLength(9, ErrorMessage = "El documento debe tener un largo maximo de 8 caracteres")]
        [MinLength(3, ErrorMessage = "El documento debe tener un largo minimo de 3 caracteres")]
        [Index(5)]
        public string nro_doc { get; set; }
        [Index(6)]
        public string PhotoFileName { get; set; }
    }
}
