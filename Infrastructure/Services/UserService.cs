using AutoMapper;
using Ecommerce.Api.Application.Users.Dtos;
using Ecommerce.Api.Application.Users.Services;
using Ecommerce.Api.Domain.Users;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UserService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> GetProfile(string userId)
        {
            var user = await _db.Users.FindAsync(userId);
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<UserProfileDto> UpdateProfile(string userId, UpdateProfileRequest request)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null) return null;

            user.Name = request.Name;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<bool> UpdatePhone(string userId, UpdatePhoneRequest request)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            // Atualiza telefone
            user.Phone = request.PhoneNumber;
            user.PhoneVerified = false;

            // Gerar código OTP
            var code = new Random().Next(100000, 999999).ToString();

            var otp = new PhoneVerification
            {
                UserId = user.Id,
                PhoneNumber = request.PhoneNumber,
                Code = code
            };

            _db.PhoneVerifications.Add(otp);
            await _db.SaveChangesAsync();

            // >>> Enviar via SMS futuramente <<<

            Console.WriteLine($"OTP gerado (debug): {code}");

            return true;
        }

        public async Task<bool> VerifyPhone(string userId, VerifyPhoneRequest request)
        {
            var otp = await _db.PhoneVerifications
                .Where(x => x.UserId == userId && x.PhoneNumber == request.PhoneNumber && !x.Used)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (otp == null) return false;
            if (otp.Code != request.OtpCode) return false;
            if (otp.ExpiresAt < DateTime.UtcNow) return false;

            otp.Used = true;

            var user = await _db.Users.FindAsync(userId);
            user.PhoneVerified = true;

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAccount(string userId, DeleteAccountRequest request)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
