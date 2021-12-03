using DataAccessLayer.Dtos.PuertaAccesos;
using DataAccessLayer.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.UsuarioPuerta
{
    public class UsuarioPuertaReadDto
    {
        public int Id { get; set; }

        public UsuarioReadDto Usuario { get; set; }

        public PuertaReadDto Puerta { get; set; }
    }
}
