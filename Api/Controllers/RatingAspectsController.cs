using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/rating-aspects")]
public class RatingAspectsController : ControllerBase
{
    private readonly IRatingAspectService _service;

    public RatingAspectsController(IRatingAspectService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAll());

    [HttpPost]
    public async Task<IActionResult> Create(CreateRatingAspectRequest request) =>
        Ok(await _service.Create(request.Name));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateRatingAspectRequest request)
    {
        var aspect = await _service.Update(id, request);
        return aspect == null ? NotFound() : Ok(aspect);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.Delete(id);
        return deleted ? Ok() : NotFound();
    }
}
