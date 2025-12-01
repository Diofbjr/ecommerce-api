namespace Ecommerce.Api.Application.Users.Dtos
{
    public class UserProfileDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }
        public bool EmailVerified { get; set; }

        public string? Phone { get; set; }
        public bool PhoneVerified { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
