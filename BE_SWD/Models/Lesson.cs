using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;
        /// <summary>
        /// Đường dẫn video của bài học (lưu URL video Firebase)
        /// </summary>
        public string? Content { get; set; }
        public int OrderIndex { get; set; } = 0;
        public bool Status { get; set; } = true;
    }
} 