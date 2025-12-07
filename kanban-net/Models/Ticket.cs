using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace kanban_net.Models
{
    public class Ticket : IHasTimeStamps
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [ForeignKey("created_by")]
        [Column("created_by")]
        public required long CreatedById { get; set; }
        public virtual User? CreatedBy { get; set; }

        [ForeignKey("assigned_to")]
        [Column("assigned_to")]
        public long? AssignedToId { get; set; }
        public virtual User? AssignedTo { get; set; }

        [Required]
        [Length(3, 255)]
        public required string Title { get; set; }

        [Column(TypeName = "TEXT")]
        public string? Details { get; set; }

        [Required]
        public required TicketStatus Status { get; set; } = TicketStatus.TO_DO;

        [Required]
        public required TicketPriority Priority { get; set; } = TicketPriority.MEDIUM;

        [Required]
        [Column("created_at", TypeName = "TIMESTAMP(6)")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        [Column("updated_at", TypeName = "TIMESTAMP(6)")]
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public enum TicketStatus
        {
            TO_DO,
            IN_PROGRESS,
            IN_REVIEW,
            DONE
        }

        public enum TicketPriority
        {
            LOW,
            MEDIUM,
            HIGH,
        }
    }
}
