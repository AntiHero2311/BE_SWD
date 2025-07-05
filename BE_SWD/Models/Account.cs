using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = null!;
        public int? MathCenterId { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 