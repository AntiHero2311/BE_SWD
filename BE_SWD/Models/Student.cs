using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = null!;
        [MaxLength(20)]
        public string? Phone { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
        public bool Status { get; set; } = true;
    }
} 