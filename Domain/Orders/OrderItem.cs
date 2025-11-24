namespace Ecommerce.Api.Domain.Orders
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string OrderId { get; set; }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Total => Quantity * UnitPrice;
    }
}
