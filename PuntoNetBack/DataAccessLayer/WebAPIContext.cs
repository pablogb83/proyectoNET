using Microsoft.EntityFrameworkCore;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext(DbContextOptions<WebAPIContext> options) : base(options)
        {
        }

        public virtual DbSet<Institucion> Instituciones { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

    }
}
