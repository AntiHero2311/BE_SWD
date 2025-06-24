using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public int Grade { get; set; } // 1-5
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
} 