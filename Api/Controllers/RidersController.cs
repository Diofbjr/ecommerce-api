using Microsoft.AspNetCore.Mvc;
using Ecommerce.Api.Application.Orders.Services;

namespace Ecommerce.Api.Api.Controllers
{
    [ApiController]
    [Route("api/riders")]
    public class RidersController : ControllerBase
    {
        private readonly IOrderTrackingService _tracking;
        public RidersController(IOrderTrackingService tracking) => _tracking = tracking;

        // GET /api/riders/{id} - basic rider status (example)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            // For now we only return a stubbed response using tracking service
            // In a full system you'd have RiderRepository/Service.
            var fake = new {
                Id = id,
                Name = $"Rider {id}",
                Status = "online",
                CurrentOrders = 2
            };
            return Ok(fake);
        }
    }
}
