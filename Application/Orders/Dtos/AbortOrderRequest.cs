namespace Ecommerce.Api.Application.Orders.Dtos
{
    public class AbortOrderRequest
    {
        public string Reason { get; set; } = string.Empty;
        public string? Comments { get; set; }
    }
}
