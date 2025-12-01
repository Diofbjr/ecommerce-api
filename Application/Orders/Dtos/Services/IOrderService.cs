using Ecommerce.Api.Domain.Orders;
using Ecommerce.Api.Application.Orders.Dtos;

public interface IOrderService
{
    // ---------------------------------------------
    // ORDERS LIST + BASIC DETAILS
    // ---------------------------------------------
    Task<List<OrderResponse>> GetActiveOrders(string userId);
    Task<List<OrderResponse>> GetPastOrders(string userId);
    Task<OrderResponse?> GetOrderById(string userId, string orderId);
    // ---------------------------------------------
    // ORDER DETAILS COMPLETOS
    // ---------------------------------------------
    Task<OrderDetailsResponse?> GetOrderDetailsAsync(string orderId, string userId);
    // ---------------------------------------------
    // REVIEW
    // ---------------------------------------------
    Task<OrderReview?> AddReview(string userId, string orderId, AddReviewRequest request);
    // ---------------------------------------------
    // CANCELAMENTO
    // ---------------------------------------------
    Task<bool> AbortOrderAsync(string orderId, string userId, AbortOrderRequest request);
    // ---------------------------------------------
    // MENSAGENS
    // ---------------------------------------------
    Task<MessageResponse> AddMessageAsync(string orderId, string userId, CreateMessageRequest request);
    Task<List<MessageResponse>> GetMessagesAsync(string orderId, string userId);
    // ---------------------------------------------
    // TRACKING INFO
    // ---------------------------------------------
    Task<TrackingInfoResponse?> GetTrackingInfoAsync(string orderId);
}
