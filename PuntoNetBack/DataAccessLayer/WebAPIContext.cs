﻿using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class WebAPIContext : MultiTenantDbContext
    {
        public WebAPIContext(ITenantInfo tenantInfo, DbContextOptions options) : base(tenantInfo, options)
        {
        }

        public virtual DbSet<Institucion> Instituciones { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

    }
}