using Ecommerce.Api.Domain.Orders;

public class OrderResponse
{
    public string Id { get; set; }
    public string OrderNumber { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public decimal TotalAmount { get; set; }

    public Restaurant Restaurant { get; set; }
    public Store Store { get; set; }
    public string ShopType { get; set; }

    public List<OrderItem> Items { get; set; }
    public OrderReview? Review { get; set; }
}
