using System.ComponentModel.DataAnnotations;
using kanban_net.Models;
using static kanban_net.Models.Ticket;

namespace kanban_net.DTOs
{
    public class TicketDTO(Ticket ticket)
    {
        public long Id { get; set; } = ticket.Id;

        public long CreatedById { get; set; } = ticket.CreatedById;

        public long? AssignedToId { get; set; } = ticket.AssignedToId;

        public string Title { get; set; } = ticket.Title;

        public string? Details { get; set; } = ticket.Details;

        public string Status { get; set; } = ticket.Status.ToString();

        public string Priority { get; set; } = ticket.Priority.ToString();

        public DateTimeOffset CreatedAt { get; set; } = ticket.CreatedAt;

        public DateTimeOffset UpdatedAt { get; set; } = ticket.UpdatedAt;
    }
}