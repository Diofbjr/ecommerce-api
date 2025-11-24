using Ecommerce.Api.Domain.Tickets;

public interface ITicketService
{
    Task<TicketResponse> CreateTicket(string userId, CreateTicketRequest request);
    Task<List<TicketResponse>> GetTickets(string userId);
    Task<TicketResponse?> GetTicketById(string userId, string ticketId);
    Task<TicketMessage?> AddMessage(string userId, string ticketId, string content);
    Task<bool> CloseTicket(string userId, string ticketId);
}
