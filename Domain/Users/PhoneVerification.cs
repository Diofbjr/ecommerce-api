namespace Ecommerce.Api.Domain.Users
{
    public class PhoneVerification
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }

        public string PhoneNumber { get; set; }
        public string Code { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(10);

        public bool Used { get; set; } = false;
    }
}
