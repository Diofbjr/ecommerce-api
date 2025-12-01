using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Api.Application.Users.Services;
using Ecommerce.Api.Application.Users.Dtos;

namespace Ecommerce.Api.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize] // 🔥 Agora exige token JWT
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // Extrai o ID do usuário a partir do JWT
        private string GetUserId()
        {
            return User.FindFirst("sub")?.Value!;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _service.GetProfile(GetUserId());
            return Ok(profile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var updated = await _service.UpdateProfile(GetUserId(), request);
            return Ok(updated);
        }

        [HttpPut("profile/phone")]
        public async Task<IActionResult> UpdatePhone([FromBody] UpdatePhoneRequest request)
        {
            var ok = await _service.UpdatePhone(GetUserId(), request);
            return ok ? Ok(new { message = "OTP enviado" }) : BadRequest();
        }

        [HttpPost("verify-phone")]
        public async Task<IActionResult> VerifyPhone([FromBody] VerifyPhoneRequest request)
        {
            var ok = await _service.VerifyPhone(GetUserId(), request);
            return ok ? Ok(new { message = "Telefone verificado" }) : BadRequest();
        }

        [HttpDelete("account")]
        public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountRequest request)
        {
            var ok = await _service.DeleteAccount(GetUserId(), request);
            return ok ? Ok(new { message = "Conta deletada" }) : BadRequest();
        }
    }
}
