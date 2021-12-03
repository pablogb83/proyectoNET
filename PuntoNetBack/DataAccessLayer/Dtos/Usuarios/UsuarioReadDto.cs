using DataAccessLayer.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Usuarios
{
    public class UsuarioReadDto
    {

        public int Id { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Role { get; set; }

    }
}
