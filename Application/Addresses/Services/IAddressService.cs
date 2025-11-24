using Ecommerce.Api.Application.Addresses.Dtos;

namespace Ecommerce.Api.Application.Addresses.Services
{
    public interface IAddressService
    {
        Task<List<AddressDto>> GetUserAddresses(string userId);
        Task<AddressDto> CreateAddress(string userId, CreateAddressRequest request);
        Task<AddressDto> UpdateAddress(string id, UpdateAddressRequest request, string userId);
        Task<bool> DeleteAddress(string addressId);
    }
}
