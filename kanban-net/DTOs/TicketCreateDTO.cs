using System.ComponentModel.DataAnnotations;
using kanban_net.Models;
using static kanban_net.Models.Ticket;

namespace kanban_net.DTOs
{
    public class TicketCreateDTO
    {
        [Required]
        [NotBlank]
        [Length(3, 255)]
        public required string Title { get; set; }

        [MaxLength(2000)]
        public string Details { get; set; } = "";
    }
}