using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewsController(IReviewService service)
    {
        _service = service;
    }

    [HttpPost("{entityId}")]
    public async Task<IActionResult> Create(string entityId, CreateReviewRequest request)
    {
        var userId = "fake-user-123";
        var review = await _service.CreateReview(userId, entityId, request);
        return Ok(review);
    }

    [HttpGet("{entityId}")]
    public async Task<IActionResult> Get(string entityId)
    {
        var list = await _service.GetReviews(entityId);
        return Ok(list);
    }

    [HttpGet("{entityId}/summary")]
    public async Task<IActionResult> Summary(string entityId)
    {
        return Ok(await _service.GetReviewSummary(entityId));
    }
}
