using Ecommerce.Api.Domain.Orders;

namespace Ecommerce.Api.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(string id);
        Task SaveChangesAsync();

        // Messages (simple in-db approach)
        Task AddOrderMessageAsync(OrderMessage message);
        Task<List<OrderMessage>> GetOrderMessagesAsync(string orderId);

        Task UpdateOrderAsync(Order order);
    }
}
