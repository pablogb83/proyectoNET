using DataAccessLayer.Dtos.Edificios;
using DataAccessLayer.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.UsuarioEdificio
{
    public class UsuarioEdificioReadDto
    {
        public int Id { get; set; }

        public UsuarioReadDto Usuario { get; set; }
        
        public EdificiosReadDto Edificio { get; set; }
    }
}
