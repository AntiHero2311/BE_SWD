using System.ComponentModel.DataAnnotations;

namespace BE_SWD.Models.DTOs
{
    public class SigninRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
} 