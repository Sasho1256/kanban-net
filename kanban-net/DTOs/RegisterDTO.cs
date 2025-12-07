using System.ComponentModel.DataAnnotations;
using kanban_net.Models;

namespace kanban_net.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [NotBlank]
        [Length(3, 50)]
        public required string Username { get; set; }

        [Required]
        [NotBlank]
        [MinLength(8)]
        [RegularExpression(pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*?-]).{8,}$", ErrorMessage = "Weak password.")]
        public required string Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}