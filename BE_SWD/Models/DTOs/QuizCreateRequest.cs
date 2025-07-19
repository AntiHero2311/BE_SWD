namespace BE_SWD.Models.DTOs
{
    public class QuizCreateRequest
    {
        public int LessonId { get; set; }
        public string Title { get; set; } = null!;
        public bool Status { get; set; } = true;
    }
} 