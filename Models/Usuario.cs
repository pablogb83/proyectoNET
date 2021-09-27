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
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}