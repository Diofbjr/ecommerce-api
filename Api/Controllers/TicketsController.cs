using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _service;

    public TicketsController(ITicketService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(CreateTicketRequest request)
    {
        var userId = "fake-user-123";
        var result = await _service.CreateTicket(userId, request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
        var userId = "fake-user-123";
        var list = await _service.GetTickets(userId);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(string id)
    {
        var userId = "fake-user-123";
        var ticket = await _service.GetTicketById(userId, id);

        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpPost("{id}/messages")]
    public async Task<IActionResult> AddMessage(string id, AddMessageRequest request)
    {
        var userId = "fake-user-123";
        var msg = await _service.AddMessage(userId, id, request.Content);

        if (msg == null) return BadRequest("Ticket not found or closed.");

        return Ok(msg);
    }

    [HttpPut("{id}/close")]
    public async Task<IActionResult> CloseTicket(string id)
    {
        var userId = "fake-user-123";
        var result = await _service.CloseTicket(userId, id);

        if (!result) return NotFound();

        return Ok(new { message = "Ticket closed" });
    }
}
