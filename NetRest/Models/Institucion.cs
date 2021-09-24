using System.ComponentModel.DataAnnotations;

namespace ProyectoNET.Models
{
    public class Institucion
    { 
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
    }
    
}