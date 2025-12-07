
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static kanban_net.Models.User;

namespace kanban_net.Models
{
    public class UserDTO(User user)
    {
        public long Id { get; set; } = user.Id;

        public string Username { get; set; } = user.Username;

        public string Role { get; set; } = user.Role.ToString();

        public string? Email { get; set; } = user.Email;

        public string Status { get; set; } = user.Status.ToString();

        public DateTimeOffset CreatedAt { get; set; } = user.CreatedAt;

        public DateTimeOffset UpdatedAt { get; set; } = user.UpdatedAt;
    }
}
