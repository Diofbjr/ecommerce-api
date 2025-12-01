using Ecommerce.Api.Domain.Orders;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;

        public async Task<Order?> GetByIdAsync(string id)
        {
            return await _db.Orders
                .Include(o => o.Items)
                .Include(o => o.Restaurant)
                .Include(o => o.Store)
                .Include("Review")
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

        public async Task AddOrderMessageAsync(OrderMessage message)
        {
            _db.Set<OrderMessage>().Add(message);
            await _db.SaveChangesAsync();
        }

        public async Task<List<OrderMessage>> GetOrderMessagesAsync(string orderId)
        {
            return await _db.Set<OrderMessage>()
                .Where(m => m.OrderId == orderId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
        }
    }
}
