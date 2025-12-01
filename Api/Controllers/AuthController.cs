using Microsoft.AspNetCore.Mvc;
using Ecommerce.Api.Application.Auth.Services;
using Ecommerce.Api.Application.Auth.Dtos;

namespace Ecommerce.Api.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                return Ok(await _auth.Register(request));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                return Ok(await _auth.Login(request));
            }
            catch
            {
                return Unauthorized(new { error = "Invalid credentials" });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            try
            {
                return Ok(await _auth.RefreshToken(request.RefreshToken));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            var result = await _auth.Logout(request.RefreshToken);
            if (!result) return NotFound();

            return Ok(new { message = "Logged out" });
        }

        [HttpPost("logout-all")]
        public async Task<IActionResult> LogoutAll()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (userId == null) return Unauthorized();

            var result = await _auth.LogoutAll(userId);
            return Ok(new { message = "All sessions terminated." });
        }
    }
}
