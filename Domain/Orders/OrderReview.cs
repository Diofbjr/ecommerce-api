namespace Ecommerce.Api.Domain.Orders
{
    public class OrderReview
    {
        public string Id { get; set; }
        public string OrderId { get; set; }

        public int Rating { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public List<string>? Aspects { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
