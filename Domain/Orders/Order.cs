namespace Ecommerce.Api.Domain.Orders
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string OrderNumber { get; set; }
        public string Status { get; set; } // pending, accepted, assigned, picked, in_transit, delivered, completed, cancelled
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public decimal TotalAmount { get; set; }

        public Restaurant Restaurant { get; set; }
        public Store Store { get; set; }
        public string ShopType { get; set; } // restaurant | store

        public List<OrderItem> Items { get; set; } = new();
        public OrderReview? Review { get; set; }
    }
}
