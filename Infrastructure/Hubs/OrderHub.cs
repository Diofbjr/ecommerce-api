using Microsoft.AspNetCore.SignalR;
using Ecommerce.Api.Application.Orders.Services;

namespace Ecommerce.Api.Infrastructure.Hubs
{
    public class OrderHub : Hub
    {
        private readonly IOrderChatService _chat;
        private readonly IOrderTrackingService _tracking;

        public OrderHub(IOrderChatService chat, IOrderTrackingService tracking)
        {
            _chat = chat;
            _tracking = tracking;
        }

        // Clients join a group per order
        public Task JoinOrderGroup(string orderId)
            => Groups.AddToGroupAsync(Context.ConnectionId, GetOrderGroupName(orderId));

        public Task LeaveOrderGroup(string orderId)
            => Groups.RemoveFromGroupAsync(Context.ConnectionId, GetOrderGroupName(orderId));

        public async Task SendMessage(string orderId, string senderId, string senderName, string content)
        {
            var msg = await _chat.SendMessageAsync(orderId, senderId, senderName, content);
            await Clients.Group(GetOrderGroupName(orderId)).SendAsync("ReceiveMessage", msg);
        }

        public async Task UpdateLocation(string orderId, string riderId, double lat, double lng)
        {
            await _tracking.UpdateRiderLocationAsync(orderId, riderId, lat, lng);
            var info = await _tracking.GetTrackingInfoAsync(orderId);
            await Clients.Group(GetOrderGroupName(orderId)).SendAsync("TrackingUpdated", info);
        }

        private static string GetOrderGroupName(string orderId) => $"order-{orderId}";
    }
}
