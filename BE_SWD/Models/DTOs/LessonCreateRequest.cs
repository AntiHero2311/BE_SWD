namespace BE_SWD.Models.DTOs
{
    public class LessonCreateRequest
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public bool Status { get; set; } = true;
    }
} 