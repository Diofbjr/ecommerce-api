namespace Ecommerce.Api.Application.Users.Dtos
{
    public class DeleteAccountRequest
    {
        public string Reason { get; set; }
        public string? AdditionalComments { get; set; }
    }
}
