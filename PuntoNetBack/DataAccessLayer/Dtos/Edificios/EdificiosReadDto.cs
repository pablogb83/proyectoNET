using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Edificios
{
    public class EdificiosReadDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
        public string lng { get; set; }

        public string lat { get; set; }
    }
}
