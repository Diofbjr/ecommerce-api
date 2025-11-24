using AutoMapper;
using Ecommerce.Api.Application.Addresses.Dtos;
using Ecommerce.Api.Application.Addresses.Services;
using Ecommerce.Api.Domain.Addresses;
using Ecommerce.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public AddressService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<AddressDto>> GetUserAddresses(string userId)
        {
            var addresses = await _db.Addresses
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<AddressDto>>(addresses);
        }

        public async Task<AddressDto> CreateAddress(string userId, CreateAddressRequest request)
        {
            var address = new Address
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Label = request.Label,
                DeliveryAddress = request.DeliveryAddress,
                Details = request.Details,
                AddressType = request.AddressType,
                CreatedAt = DateTime.UtcNow
            };

            _db.Addresses.Add(address);
            await _db.SaveChangesAsync();

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<AddressDto> UpdateAddress(string id, UpdateAddressRequest request, string userId)
        {
            var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

            if (address == null)
                return null;

            address.Label = request.Label;
            address.DeliveryAddress = request.DeliveryAddress;
            address.Details = request.Details;
            address.AddressType = request.AddressType;

            await _db.SaveChangesAsync();

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<bool> DeleteAddress(string addressId)
        {
            var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);

            if (address == null)
                return false;

            _db.Remove(address);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
