namespace TravelAPI.DTO.User
{
    public class UpdateUserDto
    {
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
    }
}
