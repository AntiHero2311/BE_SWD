using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_SWD.Models
{
    public class QuizProgress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EnrollmentId { get; set; }
        [Required]
        public int QuizId { get; set; }
        [Required]
        public double Score { get; set; }
        [Required]
        public int AttemptNumber { get; set; }
        public DateTime AttemptDate { get; set; } = DateTime.Now;
    }
} 