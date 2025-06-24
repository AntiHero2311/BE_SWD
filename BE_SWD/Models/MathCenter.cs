using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models
{
    public class MathCenter
    {
        [Key]
        public int MathCenterId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public ICollection<User>? Users { get; set; }
    }
} 