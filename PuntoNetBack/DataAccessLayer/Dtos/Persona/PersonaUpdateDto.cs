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

        [Required]
        [MaxLength(250)]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public TipoDocumento tipo_doc { get; set; }
        [Required]
        public string nro_doc { get; set; }

        public string PhotoFileName { get; set; }
    }
}
