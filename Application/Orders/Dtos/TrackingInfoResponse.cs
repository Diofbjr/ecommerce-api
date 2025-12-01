namespace Ecommerce.Api.Application.Orders.Dtos
{
    public class TrackingInfoResponse
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string? RiderId { get; set; }
        public string? RiderName { get; set; }

        public string? CurrentLocation { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime? LastUpdate { get; set; }
        public DateTime? Eta { get; set; }
    }

}
