namespace Ecommerce.Api.Domain.Tickets
{
    public class Ticket
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }

        public string Subject { get; set; }
        public string Category { get; set; } // delivery, payment, refund, app_issue, other
        public string Status { get; set; } = "open"; // open, in_progress, resolved, closed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        public List<TicketMessage> Messages { get; set; } = new();
    }
}
