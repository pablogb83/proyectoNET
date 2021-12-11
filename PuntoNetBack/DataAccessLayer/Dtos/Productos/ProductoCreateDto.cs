using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Productos
{
    public class ProductoCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar una descripcion")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Debe ingresar un precio")]
        [Range(minimum:1,maximum:1000000,ErrorMessage ="El precio debe estar entre 1 y 1000000")]
        public double Precio { get; set; }
    }
}
