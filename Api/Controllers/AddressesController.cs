using Ecommerce.Api.Application.Addresses.Dtos;
using Ecommerce.Api.Application.Addresses.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/addresses")]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _service;

    public AddressesController(IAddressService service)
    {
        _service = service;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserAddresses(string userId)
    {
        var result = await _service.GetUserAddresses(userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request)
    {
        var userId = "fake-user-123"; // JWT depois
        var address = await _service.CreateAddress(userId, request);
        return Ok(address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(string id, [FromBody] UpdateAddressRequest request)
    {
        var userId = "fake-user-123"; // depois usar JWT real

        var updated = await _service.UpdateAddress(id, request, userId);

        if (updated == null)
            return NotFound(new { message = "Address not found" });

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(string id)
    {
        var deleted = await _service.DeleteAddress(id);
        if (!deleted) return NotFound();

        return Ok(new { message = "Deleted" });
    }
}
