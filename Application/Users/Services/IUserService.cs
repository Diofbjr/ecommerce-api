using Ecommerce.Api.Application.Users.Dtos;

namespace Ecommerce.Api.Application.Users.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetProfile(string userId);
        Task<UserProfileDto> UpdateProfile(string userId, UpdateProfileRequest request);

        Task<bool> UpdatePhone(string userId, UpdatePhoneRequest request);
        Task<bool> VerifyPhone(string userId, VerifyPhoneRequest request);

        Task<bool> DeleteAccount(string userId, DeleteAccountRequest request);
    }
}
