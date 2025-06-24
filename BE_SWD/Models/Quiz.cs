using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        [Required]
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<UserQuizResult>? UserQuizResults { get; set; }
    }
} 