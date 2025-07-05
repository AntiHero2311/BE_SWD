namespace BE_SWD.Models.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? CenterName { get; set; }
        public bool IsVerified { get; set; }
    }
} 