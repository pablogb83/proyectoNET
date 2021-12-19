using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Instituciones
{
    public class InstitucionesReadDto
    {

        public string Id { get; set; }

        public string Identifier { get; set; }

        public string Name { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

    }
}
