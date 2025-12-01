using Ecommerce.Api.Application.Users.Repositories;
using Ecommerce.Api.Domain.Users;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByIdAsync(string id)
            => await _db.Users.Include(u => u.RefreshTokens)
                              .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        public async Task<User?> GetByEmailAsync(string email)
            => await _db.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);

        public async Task AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task AddRefreshTokenAsync(RefreshToken token)
        {
            _db.RefreshTokens.Add(token);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
            => await _db.RefreshTokens.Include(r => r.User)
                                    .FirstOrDefaultAsync(t => t.Token == token);

        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync();
    }
}
