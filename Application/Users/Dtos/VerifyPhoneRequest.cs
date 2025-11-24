namespace Ecommerce.Api.Application.Users.Dtos
{
    public class VerifyPhoneRequest
    {
        public string PhoneNumber { get; set; }
        public string OtpCode { get; set; }
    }
}
