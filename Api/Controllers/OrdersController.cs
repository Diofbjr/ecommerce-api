using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var userId = "fake-user-123";
        return Ok(await _service.GetActiveOrders(userId));
    }

    [HttpGet("past")]
    public async Task<IActionResult> GetPast()
    {
        var userId = "fake-user-123";
        return Ok(await _service.GetPastOrders(userId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var userId = "fake-user-123";
        var order = await _service.GetOrderById(userId, id);
        return order == null ? NotFound() : Ok(order);
    }

    [HttpPost("{orderId}/review")]
    public async Task<IActionResult> AddReview(string orderId, AddReviewRequest request)
    {
        var userId = "fake-user-123";
        var result = await _service.AddReview(userId, orderId, request);

        if (result == null)
            return BadRequest(new { error = "Cannot review this order." });

        return Ok(result);
    }
}
