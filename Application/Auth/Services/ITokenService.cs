using Ecommerce.Api.Domain.Users;

namespace Ecommerce.Api.Application.Auth.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(string userId, int days);
    }
}
