using AutoMapper;
using Ecommerce.Api.Application.Users.Dtos;
using Ecommerce.Api.Domain.Users;

namespace Ecommerce.Api.Infrastructure.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserProfileDto>();
        }
    }
}
