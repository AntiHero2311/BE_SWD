using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!; // Admin, User, MathCenter
        public string? Email { get; set; }
        public int? MathCenterId { get; set; }
        public MathCenter? MathCenter { get; set; }
        public ICollection<UserQuizResult>? QuizResults { get; set; }
    }
} 