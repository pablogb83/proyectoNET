using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Dtos.Noticias
{
    public class NoticiaCreateDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string PublicadoPor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string PhotoFileName { get; set; }
    }
}
