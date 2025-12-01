namespace Ecommerce.Api.Domain.Users
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }
        public bool EmailVerified { get; set; }

        public string? Phone { get; set; }
        public bool PhoneVerified { get; set; }

        public string PasswordHash { get; set; }

        public string UserType { get; set; } = "default";

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
