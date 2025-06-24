using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public ICollection<Quiz>? Quizzes { get; set; }
    }
} 