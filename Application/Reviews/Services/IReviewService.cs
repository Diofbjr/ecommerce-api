using Ecommerce.Api.Domain.Reviews;

public interface IReviewService
{
    Task<Review> CreateReview(string userId, string entityId, CreateReviewRequest request);
    Task<List<Review>> GetReviews(string entityId);
    Task<object> GetReviewSummary(string entityId);
}
