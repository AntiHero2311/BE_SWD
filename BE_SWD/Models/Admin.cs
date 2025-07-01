using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = null!;
    }
} 