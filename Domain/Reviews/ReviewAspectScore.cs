namespace Ecommerce.Api.Domain.Reviews
{
    public class ReviewAspectScore
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ReviewId { get; set; }

        public string AspectId { get; set; }
        public int Score { get; set; }     // 1 a 5
    }
}
