using System.ComponentModel.DataAnnotations;

namespace kanban_net.DTOs
{
    public class UserUpdateDTO
    {
        [Length(3, 50)]
        public string? Username { get; set; }

        [MinLength(8)]
        [RegularExpression(pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*?-]).{8,}$", ErrorMessage = "Weak password.")]
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Role { get; set; }

        public string? Status { get; set; }
    }
}