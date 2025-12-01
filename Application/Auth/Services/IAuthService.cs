using Ecommerce.Api.Application.Auth.Dtos;

namespace Ecommerce.Api.Application.Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(RegisterRequest request);
        Task<AuthResponse> Login(LoginRequest request);
        Task<AuthResponse> RefreshToken(string refreshToken);
        Task<bool> Logout(string refreshToken);
        Task<bool> LogoutAll(string userId);
    }
}
