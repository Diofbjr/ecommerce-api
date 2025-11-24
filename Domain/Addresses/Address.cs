namespace Ecommerce.Api.Domain.Addresses;

public class Address
{
    public string Id { get; set; }
    public string UserId { get; set; }

    public string Label { get; set; }
    public string DeliveryAddress { get; set; }
    public string? Details { get; set; }
    public string AddressType { get; set; } // House, Office, etc.

    public DateTime CreatedAt { get; set; }
}
