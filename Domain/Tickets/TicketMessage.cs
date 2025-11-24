namespace Ecommerce.Api.Domain.Tickets
{
    public class TicketMessage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string TicketId { get; set; }
        public string Sender { get; set; } // user or support
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
