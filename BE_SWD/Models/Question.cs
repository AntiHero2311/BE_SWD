using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int QuizId { get; set; }
        [Required]
        public string QuestionText { get; set; } = null!;
        public int OrderIndex { get; set; } = 0;
        public bool Status { get; set; } = true;
    }
} 