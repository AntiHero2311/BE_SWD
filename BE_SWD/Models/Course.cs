using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public int CreatedByAccountId { get; set; }
        public int? MathCenterId { get; set; }
        public bool IsVerified { get; set; } = false;
        public int? VerifiedById { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
    }
} 