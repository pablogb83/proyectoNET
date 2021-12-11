using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.Edificios
{
    public class EdificioUpdateDto
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
