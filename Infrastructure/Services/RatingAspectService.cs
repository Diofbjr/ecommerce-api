using Ecommerce.Api.Domain.Reviews;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class RatingAspectService : IRatingAspectService
{
    private readonly AppDbContext _db;

    public RatingAspectService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<RatingAspect>> GetAll()
    {
        return await _db.RatingAspects.ToListAsync();
    }

    public async Task<RatingAspect> Create(string name)
    {
        var aspect = new RatingAspect { Name = name };
        _db.RatingAspects.Add(aspect);
        await _db.SaveChangesAsync();
        return aspect;
    }

    public async Task<RatingAspect?> Update(string id, UpdateRatingAspectRequest request)
    {
        var aspect = await _db.RatingAspects.FindAsync(id);
        if (aspect == null) return null;

        aspect.Name = request.Name;
        aspect.Active = request.Active;

        await _db.SaveChangesAsync();
        return aspect;
    }

    public async Task<bool> Delete(string id)
    {
        var aspect = await _db.RatingAspects.FindAsync(id);
        if (aspect == null) return false;

        _db.RatingAspects.Remove(aspect);
        await _db.SaveChangesAsync();
        return true;
    }
}
