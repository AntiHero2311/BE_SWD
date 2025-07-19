namespace BE_SWD.Models.DTOs
{
    public class QuizProgressCreateRequest
    {
        public int EnrollmentId { get; set; }
        public int QuizId { get; set; }
        public double Score { get; set; }
    }
} 