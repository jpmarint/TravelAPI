using TravelAPI.DTO.User;
using TravelAPI.Models;
using AutoMapper;
using TravelAPI.DTO.Hotel;
using TravelAPI.DTO.Room;

namespace TravelAPI.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UpdateUserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, ShowUserDto>();

            CreateMap<CreateHotelDto, Hotel>();
            CreateMap<UpdateHotelDto, Hotel>();
            CreateMap<Hotel, ShowHotelDto>();
            CreateMap<Hotel, ShowHotelToTravelerDto>();

            CreateMap<Room, ShowRoomDto>();
            CreateMap<UpsertRoomDto, Room>();
        }
    }
}
