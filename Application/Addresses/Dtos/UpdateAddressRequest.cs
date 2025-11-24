namespace Ecommerce.Api.Application.Addresses.Dtos
{
    public class UpdateAddressRequest
    {
        public string Label { get; set; }
        public string DeliveryAddress { get; set; }
        public string? Details { get; set; }
        public string AddressType { get; set; } // House, Office, Apartment, Other
    }
}
