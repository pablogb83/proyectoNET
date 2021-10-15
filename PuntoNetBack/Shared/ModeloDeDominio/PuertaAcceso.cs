﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Finbuckle.MultiTenant;

namespace Shared.ModeloDeDominio
{
    public class PuertaAcceso
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Denominacion { get; set; }

        public Edificio edificio { get; set; }
    }
}
