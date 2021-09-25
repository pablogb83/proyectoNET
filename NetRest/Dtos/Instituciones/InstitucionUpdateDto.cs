using System.ComponentModel.DataAnnotations;

namespace ProyectoNET.Dtos.Instituciones
{
    public class InstitucionUpdateDto
    {   
        [Required]
        [MaxLength(250)]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        
    }
}