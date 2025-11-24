using Ecommerce.Api.Domain.Orders;

public interface IOrderService
{
    Task<List<OrderResponse>> GetActiveOrders(string userId);
    Task<List<OrderResponse>> GetPastOrders(string userId);
    Task<OrderResponse?> GetOrderById(string userId, string orderId);

    Task<OrderReview?> AddReview(string userId, string orderId, AddReviewRequest request);
}
