using Ecommerce.Api.Domain.Tickets;

public class TicketResponse
{
    public string Id { get; set; }
    public string Subject { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    public List<TicketMessage> Messages { get; set; }
}
