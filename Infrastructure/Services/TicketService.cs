using Ecommerce.Api.Domain.Tickets;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class TicketService : ITicketService
{
    private readonly AppDbContext _db;

    public TicketService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<TicketResponse> CreateTicket(string userId, CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            UserId = userId,
            Subject = request.Subject,
            Category = request.Category
        };

        ticket.Messages.Add(new TicketMessage
        {
            Sender = "user",
            Content = request.Message
        });

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();

        return ToResponse(ticket);
    }

    public async Task<List<TicketResponse>> GetTickets(string userId)
    {
        var list = await _db.Tickets
            .Where(t => t.UserId == userId)
            .Include(t => t.Messages)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return list.Select(ToResponse).ToList();
    }

    public async Task<TicketResponse?> GetTicketById(string userId, string ticketId)
    {
        var ticket = await _db.Tickets
            .Where(t => t.Id == ticketId && t.UserId == userId)
            .Include(t => t.Messages)
            .FirstOrDefaultAsync();

        return ticket == null ? null : ToResponse(ticket);
    }

    public async Task<TicketMessage?> AddMessage(string userId, string ticketId, string content)
    {
        var ticket = await _db.Tickets
            .Where(t => t.Id == ticketId && t.UserId == userId)
            .FirstOrDefaultAsync();

        if (ticket == null || ticket.Status == "closed")
            return null;

        var msg = new TicketMessage
        {
            TicketId = ticketId,
            Sender = "user",
            Content = content
        };

        _db.TicketMessages.Add(msg);

        await _db.SaveChangesAsync();
        return msg;
    }

    public async Task<bool> CloseTicket(string userId, string ticketId)
    {
        var ticket = await _db.Tickets
            .Where(t => t.Id == ticketId && t.UserId == userId)
            .FirstOrDefaultAsync();

        if (ticket == null) return false;

        ticket.Status = "closed";
        ticket.ClosedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    private TicketResponse ToResponse(Ticket t)
    {
        return new TicketResponse
        {
            Id = t.Id,
            Subject = t.Subject,
            Category = t.Category,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            ClosedAt = t.ClosedAt,
            Messages = t.Messages
        };
    }
}
