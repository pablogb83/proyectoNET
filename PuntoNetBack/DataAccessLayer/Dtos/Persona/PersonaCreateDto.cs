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
        [Index(5)]
        public string nro_doc { get; set; }
        [Index(6)]
        public string PhotoFileName { get; set; }
    }
}
