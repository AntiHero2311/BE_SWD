namespace BE_SWD.Models.DTOs
{
    public class SignupRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Email { get; set; }
        public int? MathCenterId { get; set; }
    }
} 