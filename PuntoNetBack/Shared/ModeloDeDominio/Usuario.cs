using Finbuckle.MultiTenant;
using System.ComponentModel.DataAnnotations;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
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
        [Required]
        public string TenantId { get; set; }

        public virtual Role Role { get; set; }
    }
}
