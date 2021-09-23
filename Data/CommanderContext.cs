using ProyectoNET.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoNET.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }

        public DbSet<Institucion> Instituciones { get; set; }
    }
}