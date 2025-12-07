using System.ComponentModel.DataAnnotations;
using kanban_net.Models;

namespace kanban_net.DTOs
{
    public class LoginDTO
    {
        [Required]
        [NotBlank]
        public required string Username { get; set; }

        [Required]
        [NotBlank]
        public required string Password { get; set; }
    }
}