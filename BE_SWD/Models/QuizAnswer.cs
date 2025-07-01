using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        [MaxLength(150)]
        public string AnswerText { get; set; } = null!;
        [Required]
        public bool IsCorrect { get; set; }
        public string? Explanation { get; set; }
        public bool Status { get; set; } = true;
    }
} 