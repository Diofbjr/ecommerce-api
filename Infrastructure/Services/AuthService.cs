using System.Security.Cryptography;
using Ecommerce.Api.Application.Auth.Dtos;
using Ecommerce.Api.Application.Auth.Services;
using Ecommerce.Api.Application.Users.Repositories;
using Ecommerce.Api.Domain.Users;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Api.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, ITokenService tokenService, IConfiguration config)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _config = config;
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            var existing = await _userRepo.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new InvalidOperationException("Email already in use");

            var user = new User
            {
                Name = request.Name,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                EmailVerified = false,
                PhoneVerified = false
            };

            await _userRepo.AddAsync(user);

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refresh = _tokenService.GenerateRefreshToken(
                user.Id,
                int.Parse(_config["Jwt:RefreshTokenExpirationDays"] ?? "30")
            );

            await _userRepo.AddRefreshTokenAsync(refresh);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refresh.Token
            };
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refresh = _tokenService.GenerateRefreshToken(
                user.Id,
                int.Parse(_config["Jwt:RefreshTokenExpirationDays"] ?? "30")
            );

            await _userRepo.AddRefreshTokenAsync(refresh);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refresh.Token
            };
        }

        public async Task<AuthResponse> RefreshToken(string refreshToken)
        {
            var stored = await _userRepo.GetRefreshTokenAsync(refreshToken);
            if (stored == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            if (stored.IsRevoked || stored.IsUsed)
                throw new UnauthorizedAccessException("Token expired or invalidated");

            if (stored.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token expired");

            stored.IsUsed = true;
            stored.IsRevoked = true;

            var newRefresh = _tokenService.GenerateRefreshToken(
                stored.UserId,
                int.Parse(_config["Jwt:RefreshTokenExpirationDays"] ?? "30")
            );

            await _userRepo.AddRefreshTokenAsync(newRefresh);
            await _userRepo.SaveChangesAsync();

            var user = stored.User ?? await _userRepo.GetByIdAsync(stored.UserId);
            var accessToken = _tokenService.GenerateAccessToken(user);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefresh.Token
            };
        }

        public async Task<bool> Logout(string refreshToken)
        {
            var stored = await _userRepo.GetRefreshTokenAsync(refreshToken);
            if (stored == null) return false;

            stored.IsRevoked = true;
            stored.IsUsed = true;

            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LogoutAll(string userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            foreach (var token in user.RefreshTokens)
                token.IsRevoked = true;

            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}
