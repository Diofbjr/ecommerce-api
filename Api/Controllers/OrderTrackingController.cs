using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Api.Application.Orders.Services;

namespace Ecommerce.Api.Api.Controllers
{
    [ApiController]
    [Route("api/order-tracking")]
    public class OrderTrackingController : ControllerBase
    {
        private readonly IOrderTrackingService _tracking;
        public OrderTrackingController(IOrderTrackingService tracking) => _tracking = tracking;

        // GET /api/order-tracking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var info = await _tracking.GetTrackingInfoAsync(id);
            return info == null ? NotFound() : Ok(info);
        }

        // PUT /api/order-tracking/{id} - public endpoint for rider to update location (could be authorized)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TrackingInfoRequestModel model)
        {
            // model contains riderId, lat, lng
            var ok = await _tracking.UpdateRiderLocationAsync(id, model.RiderId, model.Latitude, model.Longitude);
            return ok ? Ok() : NotFound();
        }
    }

    // small request model (place near controller or in DTOs)
    public class TrackingInfoRequestModel
    {
        public string RiderId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
