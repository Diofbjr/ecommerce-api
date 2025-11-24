namespace Ecommerce.Api.Application.Users.Dtos
{
    public class UpdateProfileRequest
    {
        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
