using System.ComponentModel.DataAnnotations;
using kanban_net.Models;
using static kanban_net.Models.Ticket;

namespace kanban_net.DTOs
{
    public class TicketUpdateDTO
    {
        public long? AssignedToId { get; set; }

        [Length(3, 255)]
        public string? Title { get; set; }

        [MaxLength(2000)]
        public string? Details { get; set; }

        public string? Status { get; set; }

        public string? Priority { get; set; }
    }
}