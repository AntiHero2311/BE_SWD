using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models
{
    public class UserQuizResult
    {
        [Key]
        public int ResultId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public int Score { get; set; }
        public DateTime DateTaken { get; set; }
    }
} 