﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModeloDeDominio
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string NombreRol { get; set; }

        public Usuario usuario { get; set; }

    }
}
