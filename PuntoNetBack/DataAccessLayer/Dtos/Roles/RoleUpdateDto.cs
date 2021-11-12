﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Dtos.Roles
{
    public class RoleUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}