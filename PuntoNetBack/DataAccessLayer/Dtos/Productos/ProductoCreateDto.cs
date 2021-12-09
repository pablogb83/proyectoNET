using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Productos
{
    public class ProductoCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
    }
}
