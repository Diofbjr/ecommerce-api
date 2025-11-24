namespace Ecommerce.Api.Application.Users.Dtos
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool PhoneVerified { get; set; }
    }
}
