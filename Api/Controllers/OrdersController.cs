using Ecommerce.Api.Application.Orders.Dtos;
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

    // =====================================================
    // GET ACTIVE ORDERS
    // =====================================================
    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var userId = "fake-user-123";
        var result = await _service.GetActiveOrders(userId);
        return Ok(result);
    }

    // =====================================================
    // GET PAST ORDERS
    // =====================================================
    [HttpGet("past")]
    public async Task<IActionResult> GetPast()
    {
        var userId = "fake-user-123";
        var result = await _service.GetPastOrders(userId);
        return Ok(result);
    }

    // =====================================================
    // ORDER BASIC INFO
    // =====================================================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var userId = "fake-user-123";
        var order = await _service.GetOrderById(userId, id);
        return order == null ? NotFound() : Ok(order);
    }

    // =====================================================
    // FULL ORDER DETAILS (Messages, Review, Items,...)
    // =====================================================
    [HttpGet("{orderId}/details")]
    public async Task<IActionResult> GetDetails(string orderId)
    {
        var userId = "fake-user-123";
        var details = await _service.GetOrderDetailsAsync(orderId, userId);

        if (details == null)
            return NotFound();

        return Ok(details);
    }

    // =====================================================
    // ADD REVIEW
    // =====================================================
    [HttpPost("{orderId}/review")]
    public async Task<IActionResult> AddReview(string orderId, [FromBody] AddReviewRequest request)
    {
        var userId = "fake-user-123";
        var result = await _service.AddReview(userId, orderId, request);

        if (result == null)
            return BadRequest(new { error = "Cannot review this order." });

        return Ok(result);
    }

    // =====================================================
    // CANCEL ORDER
    // =====================================================
    [HttpPost("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder(string orderId, [FromBody] AbortOrderRequest request)
    {
        var userId = "fake-user-123";
        var ok = await _service.AbortOrderAsync(orderId, userId, request);

        if (!ok)
            return BadRequest(new { error = "Unable to cancel this order." });

        return Ok(new { message = "Order cancelled successfully." });
    }

    // =====================================================
    // SEND MESSAGE
    // =====================================================
    [HttpPost("{orderId}/messages")]
    public async Task<IActionResult> SendMessage(string orderId, [FromBody] CreateMessageRequest request)
    {
        var userId = "fake-user-123";
        var message = await _service.AddMessageAsync(orderId, userId, request);

        if (message == null)
            return BadRequest(new { error = "Could not send message." });

        return Ok(message);
    }

    // =====================================================
    // LIST MESSAGES
    // =====================================================
    [HttpGet("{orderId}/messages")]
    public async Task<IActionResult> GetMessages(string orderId)
    {
        var userId = "fake-user-123";
        var messages = await _service.GetMessagesAsync(orderId, userId);
        return Ok(messages);
    }

    // =====================================================
    // TRACKING INFO
    // =====================================================
    [HttpGet("{orderId}/tracking")]
    public async Task<IActionResult> GetTracking(string orderId)
    {
        var tracking = await _service.GetTrackingInfoAsync(orderId);

        if (tracking == null)
            return NotFound();

        return Ok(tracking);
    }
}
