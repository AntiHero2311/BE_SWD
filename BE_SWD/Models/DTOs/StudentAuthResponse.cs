namespace BE_SWD.Models.DTOs
{
    public class StudentAuthResponse
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int UserId { get; set; }
        public int? StudentId { get; set; }
        public string? FullName { get; set; }
    }
} 