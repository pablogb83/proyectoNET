using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Roles
{
    public class RoleCreateDto
    {
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MaxLength(250)]
        public string Name { get; set; }

    }
}
