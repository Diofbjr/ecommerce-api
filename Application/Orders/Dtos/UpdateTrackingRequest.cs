namespace Ecommerce.Api.Application.Orders.Dtos
{
    public class UpdateTrackingRequest
    {
        public string? RiderId { get; set; }
        public string? RiderName { get; set; }
        public string? RiderPhone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? Eta { get; set; }
    }
}