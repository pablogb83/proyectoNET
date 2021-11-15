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

        [Required]
        [MaxLength(250)]
        [Index(0)]
        public string Nombres { get; set; }
        [Required]
        [Index(1)]
        public string Apellidos { get; set; }
        [Required]
        [Index(2)]
        public string Telefono { get; set; }
        [Required]
        [Index(3)]
        public string Email { get; set; }
        [Required]
        [Index(4)]
        public TipoDocumento tipo_doc { get; set; }
        [Required]
        [Index(5)]
        public string nro_doc { get; set; }
        [Index(6)]
        public string PhotoFileName { get; set; }
    }
}
