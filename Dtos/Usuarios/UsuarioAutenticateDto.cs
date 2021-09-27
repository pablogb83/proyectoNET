using System.ComponentModel.DataAnnotations;

namespace ProyectoNET.Dtos.Usuarios
{
    public class UsuarioAutenticateDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}