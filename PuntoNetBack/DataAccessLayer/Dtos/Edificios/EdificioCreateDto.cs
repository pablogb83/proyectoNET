using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Dtos.Edificios
{
    public class EdificioCreateDto
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [MaxLength(250)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo direccion es requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo telefono es requerido")]
        public string Telefono { get; set; }

        public string lng { get; set; }

        public string lat { get; set; }
    }
}