
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kanban_net.Models
{
    public class User : IHasTimeStamps
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Length(3, 50)]
        [NotBlank]
        public required string Username { get; set; }

        [Required]
        [NotBlank]
        public string? Password { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.USER;

        public string? Email { get; set; }

        [Required]
        public UserStatus Status { get; set; } = UserStatus.ACTIVE;

        [Required]
        [Column("created_at", TypeName = "TIMESTAMP(6)")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        [Column("updated_at", TypeName = "TIMESTAMP(6)")]
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public enum UserRole
        {
            USER,
            MANAGER,
            ADMIN,
        }

        public enum UserStatus
        {
            ACTIVE,
            INACTIVE,
        }
    }
}
