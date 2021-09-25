using System.ComponentModel.DataAnnotations;

namespace ProyectoNET.Dtos.Usuarios
{
    public class UsuarioCreateDto
    {   
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}