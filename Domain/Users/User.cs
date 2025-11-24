namespace Ecommerce.Api.Domain.Users
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool PhoneVerified { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
