using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        [Required]
        public string QuestionText { get; set; } = null!;
        [Required]
        public string AnswerOptions { get; set; } = null!; // JSON or comma-separated
        [Required]
        public string CorrectAnswer { get; set; } = null!;
    }
} 