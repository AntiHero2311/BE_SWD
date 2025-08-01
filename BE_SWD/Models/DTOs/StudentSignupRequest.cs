using System.ComponentModel.DataAnnotations;
using BE_SWD.Models.ValidationAttributes;

namespace BE_SWD.Models.DTOs
{
    public class StudentSignupRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = null!;

        [MaxLength(20)]
        [PhoneValidation]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }
    }
} 