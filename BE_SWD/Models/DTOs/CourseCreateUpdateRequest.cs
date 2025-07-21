namespace BE_SWD.Models.DTOs
{
    public class CourseCreateUpdateRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int CreatedByAccountId { get; set; }
        public int? MathCenterId { get; set; }
        public bool IsVerified { get; set; } = false;
        public int? VerifiedById { get; set; }
        public bool Status { get; set; } = true;
        public decimal Price { get; set; } = 0;
    }
} 