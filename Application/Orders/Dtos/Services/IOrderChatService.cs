using Ecommerce.Api.Application.Orders.Dtos;

namespace Ecommerce.Api.Application.Orders.Services
{
    public interface IOrderChatService
    {
        Task<MessageResponse> SendMessageAsync(string orderId, string senderId, string senderName, string content);
        Task<List<MessageResponse>> GetMessagesAsync(string orderId);
    }
}
