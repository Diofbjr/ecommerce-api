using Ecommerce.Api.Domain.Reviews;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _db;

    public ReviewService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Review> CreateReview(string userId, string entityId, CreateReviewRequest request)
    {
        var review = new Review
        {
            UserId = userId,
            EntityId = entityId,
            EntityType = request.EntityType,
            Rating = request.Rating,
            Description = request.Description,
        };

        foreach (var aspect in request.Aspects)
        {
            review.Aspects.Add(new ReviewAspectScore
            {
                AspectId = aspect.AspectId,
                Score = aspect.Score
            });
        }

        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();

        return review;
    }

    public async Task<List<Review>> GetReviews(string entityId)
    {
        return await _db.Reviews
            .Where(r => r.EntityId == entityId)
            .Include(r => r.Aspects)
            .ToListAsync();
    }

    public async Task<object> GetReviewSummary(string entityId)
    {
        var reviews = await _db.Reviews
            .Where(r => r.EntityId == entityId)
            .Include(r => r.Aspects)
            .ToListAsync();

        if (!reviews.Any())
            return new { average = 0, total = 0 };

        return new
        {
            average = Math.Round(reviews.Average(r => r.Rating), 1),
            total = reviews.Count,
            aspects = reviews
                .SelectMany(r => r.Aspects)
                .GroupBy(a => a.AspectId)
                .Select(g => new
                {
                    aspectId = g.Key,
                    average = Math.Round(g.Average(x => x.Score), 1)
                })
        };
    }
}
