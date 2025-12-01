using Ecommerce.Api.Domain.Users;

namespace Ecommerce.Api.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);

        Task AddRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);

        Task SaveChangesAsync();
    }
}
