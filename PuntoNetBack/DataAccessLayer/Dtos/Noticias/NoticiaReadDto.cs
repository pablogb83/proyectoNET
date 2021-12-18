using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.Noticias
{
    public class NoticiaReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string PublicadoPor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string PhotoFileName { get; set; }
        public string Institucion { get; set; }
    }
}
