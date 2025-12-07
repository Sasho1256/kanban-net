using kanban_net.Models;
using Microsoft.EntityFrameworkCore;

namespace kanban_net.Data
{
    public class KanbanContext(DbContextOptions<KanbanContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasColumnName("status");
                entity.Property(e => e.Priority)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasColumnName("priority");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("TIMESTAMP(6)")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("TIMESTAMP(6)")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username).IsRequired();

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Role)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasColumnName("status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("TIMESTAMP(6)")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("TIMESTAMP(6)")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
