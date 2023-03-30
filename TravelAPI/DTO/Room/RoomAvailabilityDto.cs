namespace TravelAPI.DTO.Room
{
    public class RoomAvailabilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public double PricePerNight { get; set; }
        
    }
}
