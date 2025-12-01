using Ecommerce.Api.Application.Orders.Dtos;
using Ecommerce.Api.Application.Orders.Services;
using Ecommerce.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Infrastructure.Services
{
    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly IOrderRepository _repo;
        public OrderTrackingService(IOrderRepository repo) => _repo = repo;

        public async Task<TrackingInfoResponse?> GetTrackingInfoAsync(string orderId)
        {
            var order = await _repo.GetByIdAsync(orderId);
            if (order == null) return null;

            // Formatar CurrentLocation a partir das coordenadas
            string currentLocation = null;
            if (order.CurrentLat.HasValue && order.CurrentLng.HasValue)
            {
                currentLocation = $"{order.CurrentLat.Value:F6},{order.CurrentLng.Value:F6}";
            }

            return new TrackingInfoResponse
            {
                OrderId = order.Id,
                Status = order.Status,
                RiderId = order.RiderId,
                RiderName = order.RiderName,
                CurrentLocation = currentLocation, // Formatar string
                Latitude = order.CurrentLat,       // Usar CurrentLat
                Longitude = order.CurrentLng,      // Usar CurrentLng
                LastUpdate = order.UpdatedAt,
                Eta = order.EstimatedDeliveryTime  // Usar EstimatedDeliveryTime
            };
        }

        public async Task<bool> UpdateRiderLocationAsync(string orderId, string riderId, double lat, double lng)
        {
            var order = await _repo.GetByIdAsync(orderId);
            if (order == null) return false;

            order.RiderId = riderId;
            order.CurrentLat = lat;        // Usar CurrentLat
            order.CurrentLng = lng;        // Usar CurrentLng
            // Não precisa setar CurrentLocation - é derivado das coordenadas
            order.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateOrderAsync(order);
            return true;
        }

        // MÉTODO ADICIONAL: Atualizar com objeto mais completo
        public async Task<bool> UpdateTrackingInfoAsync(string orderId, UpdateTrackingRequest request)
        {
            var order = await _repo.GetByIdAsync(orderId);
            if (order == null) return false;

            if (!string.IsNullOrEmpty(request.RiderId))
                order.RiderId = request.RiderId;
                
            if (!string.IsNullOrEmpty(request.RiderName))
                order.RiderName = request.RiderName;
                
            if (request.Latitude.HasValue)
                order.CurrentLat = request.Latitude.Value;
                
            if (request.Longitude.HasValue)
                order.CurrentLng = request.Longitude.Value;
                
            if (request.Eta.HasValue)
                order.EstimatedDeliveryTime = request.Eta.Value;
                
            order.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateOrderAsync(order);
            return true;
        }

        // MÉTODO ADICIONAL: Atualizar apenas o entregador
        public async Task<bool> AssignRiderAsync(string orderId, string riderId, string riderName, string riderPhone = null)
        {
            var order = await _repo.GetByIdAsync(orderId);
            if (order == null) return false;

            order.RiderId = riderId;
            order.RiderName = riderName;
            
            // Se sua classe Order tiver RiderPhone, descomente:
            // if (!string.IsNullOrEmpty(riderPhone))
            //     order.RiderPhone = riderPhone;
                
            order.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateOrderAsync(order);
            return true;
        }
    }
}