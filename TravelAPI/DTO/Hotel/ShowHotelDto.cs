namespace TravelAPI.DTO.Hotel
{
    public class ShowHotelDto
    {
        public required string Name { get; set; }
        public string Location { get; set; }
        public decimal Commission { get; set; }
        public bool IsActive { get; set; }
    }
}
