namespace TravelAPI.DTO.Room
{
    public class ShowRoomDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public int HotelId { get; set; }
    }
}
