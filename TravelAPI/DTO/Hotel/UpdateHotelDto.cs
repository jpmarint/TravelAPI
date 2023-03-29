namespace TravelAPI.DTO.Hotel
{
    public class UpdateHotelDto
    {
        public required string Name { get; set; }
        public string Location { get; set; }
        public decimal Commission { get; set; }
        public bool IsActive { get; set; }
    }
}
