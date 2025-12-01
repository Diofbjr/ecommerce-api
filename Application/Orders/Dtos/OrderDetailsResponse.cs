using Ecommerce.Api.Domain.Orders;

namespace Ecommerce.Api.Application.Orders.Dtos
{
    public class OrderDetailsResponse
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        // Tracking summary
        public string? RiderId { get; set; }
        public string? RiderName { get; set; }
        public string? CurrentLocation { get; set; }
        public DateTime? Eta { get; set; }

        // Relationship data
        public Restaurant Restaurant { get; set; }
        public Store Store { get; set; }
        public string ShopType { get; set; }

        public List<OrderItem> Items { get; set; } = new();
        public OrderReview? Review { get; set; }

        public List<MessageResponse> Messages { get; set; } = new();
    }
}
