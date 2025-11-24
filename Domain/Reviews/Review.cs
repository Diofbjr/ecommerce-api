namespace Ecommerce.Api.Domain.Reviews
{
    public class Review
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }
        public string EntityId { get; set; }        // ID do produto / loja / restaurante
        public string EntityType { get; set; }      // product, store, restaurant, driver

        public int Rating { get; set; }             // 1-5
        public string? Description { get; set; }

        public List<ReviewAspectScore> Aspects { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
