namespace TravelAPI.DTO.Reservation
{
    public class CreateReservationDto
    {
        public decimal TotalCost { get; set; }
        public bool IsActive { get; set; } = true;
        public int NumberOfGuests { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
