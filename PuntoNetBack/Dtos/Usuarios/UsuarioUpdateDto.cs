using System.ComponentModel.DataAnnotations;

namespace ProyectoNET.Dtos.Usuarios
{
    public class UsuarioUpdateDto
    {   
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}