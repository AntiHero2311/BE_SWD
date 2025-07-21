namespace BE_SWD.Models.DTOs
{
    public class LessonCreateRequest
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        /// <summary>
        /// Đường dẫn video của bài học (lưu URL video Firebase)
        /// </summary>
        public string? Content { get; set; }
        public bool Status { get; set; } = true;
    }
} 