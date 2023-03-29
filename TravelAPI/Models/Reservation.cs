using Microsoft.OpenApi.Models;

namespace TravelAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public decimal TotalCost { get; set; }
        public bool IsActive { get; set; } = true;
        public int NumberOfGuests { get; set; }
        public Contact Contact { get; set; }
        public User User { get; set; }
        public Room Room { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
