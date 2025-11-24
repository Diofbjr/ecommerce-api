using Microsoft.AspNetCore.Mvc;
using Ecommerce.Api.Application.Users.Services;
using Ecommerce.Api.Application.Users.Dtos;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = "fake-user-123";
        var profile = await _service.GetProfile(userId);
        return Ok(profile);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        var userId = "fake-user-123";
        var updated = await _service.UpdateProfile(userId, request);
        return Ok(updated);
    }

    [HttpPut("profile/phone")]
    public async Task<IActionResult> UpdatePhone(UpdatePhoneRequest request)
    {
        var userId = "fake-user-123";
        var ok = await _service.UpdatePhone(userId, request);
        return ok ? Ok() : BadRequest();
    }

    [HttpPost("verify-phone")]
    public async Task<IActionResult> VerifyPhone(VerifyPhoneRequest request)
    {
        var userId = "fake-user-123";
        var ok = await _service.VerifyPhone(userId, request);
        return ok ? Ok() : BadRequest();
    }

    [HttpDelete("account")]
    public async Task<IActionResult> DeleteAccount(DeleteAccountRequest request)
    {
        var userId = "fake-user-123";
        var ok = await _service.DeleteAccount(userId, request);
        return ok ? Ok() : BadRequest();
    }
}
