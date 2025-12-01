using Ecommerce.Api.Application.Orders.Dtos;
using Ecommerce.Api.Application.Orders.Services;
using Ecommerce.Api.Infrastructure.Repositories;

namespace Ecommerce.Api.Infrastructure.Services
{
    public class OrderChatService : IOrderChatService
    {
        private readonly IOrderRepository _repo;
        public OrderChatService(IOrderRepository repo) => _repo = repo;

        public async Task<MessageResponse> SendMessageAsync(string orderId, string senderId, string senderName, string content)
        {
            var message = new Domain.Orders.OrderMessage
            {
                OrderId = orderId,
                SenderId = senderId,
                SenderName = senderName,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            await _repo.AddOrderMessageAsync(message);

            return new MessageResponse
            {
                Id = message.Id,
                OrderId = message.OrderId,
                SenderId = message.SenderId,
                SenderName = message.SenderName,
                Content = message.Content,
                SentAt = message.SentAt
            };
        }

        public async Task<List<MessageResponse>> GetMessagesAsync(string orderId)
        {
            var msgs = await _repo.GetOrderMessagesAsync(orderId);
            return msgs.Select(m => new MessageResponse
            {
                Id = m.Id,
                OrderId = m.OrderId,
                SenderId = m.SenderId,
                SenderName = m.SenderName,
                Content = m.Content,
                SentAt = m.SentAt
            }).ToList();
        }
    }
}
