﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Accesos
{
    public class AccesoUpdateDto
    {
        [Required]
        public DateTime FechaHora { get; set; }
    }
}
