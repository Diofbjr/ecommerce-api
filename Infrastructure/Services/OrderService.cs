using Ecommerce.Api.Application.Orders.Dtos;
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

    #region ORDERS ATIVOS
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
    #endregion

    #region ORDERS PASSADOS
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
    #endregion

    #region GET ORDER BY ID
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
    #endregion

    #region ORDER DETAILS COMPLETOS
    public async Task<OrderDetailsResponse?> GetOrderDetailsAsync(string orderId, string userId)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .Include(o => o.Store)
            .Include(o => o.Messages)
            .Include(o => o.Review)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

        if (order == null) return null;

        return new OrderDetailsResponse
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            DeliveredAt = order.DeliveredAt,
            TotalAmount = order.TotalAmount,
            Restaurant = order.Restaurant,
            Store = order.Store,
            ShopType = order.ShopType,
            Items = order.Items.ToList(),
            Review = order.Review,
            Messages = order.Messages
                .Select(m => new MessageResponse
                {
                    Id = m.Id,
                    OrderId = m.OrderId,
                    SenderId = m.SenderId,
                    SenderName = m.SenderName,
                    Content = m.Content,
                    SentAt = m.SentAt
                }).ToList()
        };
    }
    #endregion

    #region ADD REVIEW
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
    #endregion

    #region CANCELAR PEDIDO
    public async Task<bool> AbortOrderAsync(string orderId, string userId, AbortOrderRequest request)
    {
        var order = await _db.Orders
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

        if (order == null) return false;
        if (order.Status == "delivered" || order.Status == "completed") return false;

        order.Status = "cancelled";
        order.CancellationReason = request.Reason;
        order.CancelledAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }
    #endregion

    #region ENVIAR MENSAGEM
    public async Task<MessageResponse> AddMessageAsync(string orderId, string userId, CreateMessageRequest request)
    {
        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        if (order == null) return null;

        var message = new OrderMessage
        {
            OrderId = orderId,
            SenderId = userId,
            SenderName = "User",
            Content = request.Content,
            SentAt = DateTime.UtcNow
        };

        _db.OrderMessages.Add(message);
        
        order.UpdatedAt = DateTime.UtcNow;
        
        await _db.SaveChangesAsync();

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
    #endregion

    #region LISTAR MENSAGENS
    public async Task<List<MessageResponse>> GetMessagesAsync(string orderId, string userId)
    {
        var exists = await _db.Orders.AnyAsync(o => o.Id == orderId && o.UserId == userId);
        if (!exists) return new List<MessageResponse>();

        return await _db.OrderMessages
            .Where(m => m.OrderId == orderId)
            .OrderBy(m => m.SentAt)
            .Select(m => new MessageResponse
            {
                Id = m.Id,
                OrderId = m.OrderId,
                SenderId = m.SenderId,
                SenderName = m.SenderName,
                Content = m.Content,
                SentAt = m.SentAt
            })
            .ToListAsync();
    }
    #endregion

    #region TRACKING INFO
    public async Task<TrackingInfoResponse?> GetTrackingInfoAsync(string orderId)
    {
        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return null;

        string currentLocation = null;
        if (order.CurrentLat.HasValue && order.CurrentLng.HasValue)
        {
            currentLocation = $"{order.CurrentLat.Value:F6}, {order.CurrentLng.Value:F6}";
        }

        return new TrackingInfoResponse
        {
            OrderId = order.Id,
            Status = order.Status,
            RiderId = order.RiderId,
            RiderName = order.RiderName,
            CurrentLocation = currentLocation,
            Latitude = order.CurrentLat,
            Longitude = order.CurrentLng,
            LastUpdate = order.UpdatedAt,
            Eta = order.EstimatedDeliveryTime
        };
    }

    public async Task<TrackingInfoResponse?> GetTrackingInfoAsync(string orderId, string userId)
    {
        var order = await _db.Orders
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        
        if (order == null) return null;

        string currentLocation = null;
        if (order.CurrentLat.HasValue && order.CurrentLng.HasValue)
        {
            currentLocation = $"{order.CurrentLat.Value:F6}, {order.CurrentLng.Value:F6}";
        }

        return new TrackingInfoResponse
        {
            OrderId = order.Id,
            Status = order.Status,
            RiderId = order.RiderId,
            RiderName = order.RiderName,
            CurrentLocation = currentLocation,
            Latitude = order.CurrentLat,
            Longitude = order.CurrentLng,
            LastUpdate = order.UpdatedAt,
            Eta = order.EstimatedDeliveryTime
        };
    }
    #endregion

    #region MAPPING
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
    #endregion
}