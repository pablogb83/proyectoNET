using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Usuarios
{
    public class AdminCreateDto
    {
        public string Email { get; set; }
        public string PasswordPlano { get; set; }
        public string TenantId { get; set; }
    }
}
