using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Instituciones
{
    public class InstitucionUpdateDto
    {
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debe ingresar una direccion")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Debe ingresar un telefono")]
        public string Telefono { get; set; }

    }
}
