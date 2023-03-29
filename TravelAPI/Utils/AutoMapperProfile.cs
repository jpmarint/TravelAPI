using TravelAPI.DTO.User;
using TravelAPI.Models;
using AutoMapper;

namespace TravelAPI.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UpdateUserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, ShowUserDto>();
        }
    }
}
