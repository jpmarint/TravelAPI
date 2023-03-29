namespace TravelAPI.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal Commission { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
    }
}
