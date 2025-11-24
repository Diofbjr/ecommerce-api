using Ecommerce.Api.Domain.Orders;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<OrderResponse>> GetActiveOrders(string userId)
    {
        var activeStatuses = new[] { "pending", "accepted", "assigned", "picked", "in_transit" };

        var orders = await _db.Orders
            .Where(o => o.UserId == userId && activeStatuses.Contains(o.Status))
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .Include(o => o.Store)
            .Include(o => o.Review)
            .ToListAsync();

        return orders.Select(o => Map(o)).ToList();
    }

    public async Task<List<OrderResponse>> GetPastOrders(string userId)
    {
        var pastStatuses = new[] { "delivered", "completed", "cancelled" };

        var orders = await _db.Orders
            .Where(o => o.UserId == userId && pastStatuses.Contains(o.Status))
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .Include(o => o.Store)
            .Include(o => o.Review)
            .ToListAsync();

        return orders.Select(o => Map(o)).ToList();
    }

    public async Task<OrderResponse?> GetOrderById(string userId, string orderId)
    {
        var order = await _db.Orders
            .Where(o => o.Id == orderId && o.UserId == userId)
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .Include(o => o.Store)
            .Include(o => o.Review)
            .FirstOrDefaultAsync();

        return order == null ? null : Map(order);
    }

    public async Task<OrderReview?> AddReview(string userId, string orderId, AddReviewRequest request)
    {
        var order = await _db.Orders
            .Include(o => o.Review)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

        if (order == null) return null;
        if (order.Status != "delivered" && order.Status != "completed") return null;
        if (order.Review != null) return null;

        var review = new OrderReview
        {
            OrderId = orderId,
            Rating = request.Rating,
            Description = request.Description,
            Comments = request.Comments,
            Aspects = request.Aspects
        };

        order.Review = review;

        await _db.SaveChangesAsync();
        return review;
    }

    private OrderResponse Map(Order o)
    {
        return new OrderResponse
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            Status = o.Status,
            CreatedAt = o.CreatedAt,
            DeliveredAt = o.DeliveredAt,
            TotalAmount = o.TotalAmount,
            Restaurant = o.Restaurant,
            Store = o.Store,
            ShopType = o.ShopType,
            Items = o.Items,
            Review = o.Review
        };
    }
}
