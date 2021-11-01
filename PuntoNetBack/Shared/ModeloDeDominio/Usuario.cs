using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.ModeloDeDominio
{
    [MultiTenant]
    public class Usuario : IdentityUser<int>
    {
       //public int Id { get; set; }
        [Required]
        [EmailAddress]
       // public string Email { get; set; }
        public string TenantId { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
