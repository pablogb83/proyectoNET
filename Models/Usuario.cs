using System.ComponentModel.DataAnnotations;

namespace ProyectoNET.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}