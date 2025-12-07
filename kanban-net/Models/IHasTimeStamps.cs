namespace kanban_net.Models
{
    interface IHasTimeStamps
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}