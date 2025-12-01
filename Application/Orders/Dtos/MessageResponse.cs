namespace Ecommerce.Api.Application.Orders.Dtos
{
    public class MessageResponse
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
