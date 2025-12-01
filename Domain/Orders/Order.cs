namespace Ecommerce.Api.Domain.Orders
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }

        public string OrderNumber { get; set; }
        public string Status { get; set; } // pending, accepted, assigned, picked, in_transit, delivered, completed, cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveredAt { get; set; }
        public decimal TotalAmount { get; set; }

        // ---------------------------------------------------
        //  TRACKING COMPLETO
        // ---------------------------------------------------
        public string? RiderId { get; set; }
        public string? RiderName { get; set; }
        public string? RiderPhone { get; set; }

        public double? CurrentLat { get; set; }
        public double? CurrentLng { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }

        // ---------------------------------------------------
        //  CANCELAMENTO DO PEDIDO
        // ---------------------------------------------------
        public string? CancellationReason { get; set; }
        public DateTime? CancelledAt { get; set; }

        // ---------------------------------------------------
        //  CHAT DO PEDIDO
        // ---------------------------------------------------
        public List<OrderMessage> Messages { get; set; } = new();

        // ---------------------------------------------------
        // RELACIONAMENTOS EXISTENTES
        // ---------------------------------------------------
        public Restaurant Restaurant { get; set; }
        public Store Store { get; set; }
        public string ShopType { get; set; } // restaurant | store

        public List<OrderItem> Items { get; set; } = new();
        public OrderReview? Review { get; set; }
    }
}
