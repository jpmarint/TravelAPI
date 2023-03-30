using TravelAPI.DTO.Room;

namespace TravelAPI.DTO.Hotel
{
    public class HotelAvailabilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<RoomAvailabilityDto> Rooms { get; set; }
    }
}
