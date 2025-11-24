using Ecommerce.Api.Domain.Reviews;

public interface IRatingAspectService
{
    Task<List<RatingAspect>> GetAll();
    Task<RatingAspect> Create(string name);
    Task<RatingAspect?> Update(string id, UpdateRatingAspectRequest request);
    Task<bool> Delete(string id);
}
