using DataAccessLayer.Dtos.Edificios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Salon
{
    public class SalonReadDto
    {
        public int Id { get; set; }

        public string Denominacion { get; set; }

        public int Numero { get; set; }

        public EdificiosReadDto Edificio { get; set; }
    }
}
