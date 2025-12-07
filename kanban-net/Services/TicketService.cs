using System.Data;
using kanban_net.Data;
using kanban_net.DTOs;
using kanban_net.Models;
using static kanban_net.Models.Ticket;
using static kanban_net.Models.User;

namespace kanban_net.Services
{
    public class TicketService(KanbanContext db)
    {
        private readonly KanbanContext _db = db;

        public TicketDTO Create(TicketCreateDTO data, long currentUserId)
        {
            string title = data.Title.Trim();
            string details = data.Details.Trim();

            if (!_db.Users.Any(u => u.Id == currentUserId))
            {
                throw new MissingMemberException("Current user not found in context.");
            }

            Ticket ticket = new Ticket()
            {
                Title = title,
                Details = details,
                Status = Ticket.TicketStatus.TO_DO,
                Priority = Ticket.TicketPriority.MEDIUM,
                CreatedById = 1,
                AssignedToId = 1,
            };

            _db.Tickets.Add(ticket);
            _db.SaveChanges();

            return new TicketDTO(ticket);
        }

        public TicketDTO Get(long id)
        {
            Ticket? ticket = _db.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                throw new MissingMemberException("Ticket not found.");
            }

            return new TicketDTO(ticket);
        }

        public TicketDTO[] GetAll()
        {
            List<Ticket> tickets = _db.Tickets.ToList();

            if (tickets.Count == 0)
            {
                return [];
            }

            return tickets.Select(t => new TicketDTO(t)).ToArray();
        }

        public TicketDTO Update(long id, TicketUpdateDTO data, UserRole? currentRole)
        {
            Ticket? ticket = _db.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                throw new MissingMemberException("Ticket not found.");
            }

            long? assignedToId = data.AssignedToId;
            if (assignedToId != null)
            {
                ticket.AssignedToId = assignedToId;
            }

            string? title = data.Title?.Trim();
            if (title != null)
            {
                ticket.Title = title;
            }

            string? details = data.Details?.Trim();
            if (details != null)
            {
                ticket.Details = details;
            }

            string? status = data.Status?.Trim();
            if (status != null && Enum.TryParse<TicketStatus>(data.Status, true, out var s))
            {
                if (s == TicketStatus.DONE && currentRole == UserRole.USER)
                {
                    throw new UnauthorizedAccessException("Only managers and admins can close tickets.");
                }

                ticket.Status = s;
            }

            string? priority = data.Priority?.Trim();
            if (priority != null && Enum.TryParse<TicketPriority>(priority, true, out var p))
            {
                ticket.Priority = p;
            }

            _db.SaveChanges();

            return new TicketDTO(ticket);
        }

        public void Delete(long id)
        {
            Ticket? ticket = _db.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                throw new MissingMemberException("Ticket not found.");
            }

            _db.Tickets.Remove(ticket);
            _db.SaveChanges();
        }
    }
}