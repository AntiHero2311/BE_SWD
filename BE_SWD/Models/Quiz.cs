using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;
        public int OrderIndex { get; set; } = 0;
        public bool Status { get; set; } = true;
        public Lesson? Lesson { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
} 