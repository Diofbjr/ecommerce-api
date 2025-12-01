using Ecommerce.Api.Application.Orders.Dtos;

namespace Ecommerce.Api.Application.Orders.Services
{
    public interface IOrderTrackingService
    {
        Task<TrackingInfoResponse?> GetTrackingInfoAsync(string orderId);
        Task<bool> UpdateRiderLocationAsync(string orderId, string riderId, double lat, double lng);
    }
}
