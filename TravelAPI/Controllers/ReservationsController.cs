using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAPI.Data;
using TravelAPI.DTO.Reservation;
using TravelAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAPI.Controllers
{
    /// <summary>
    /// Controller for handling reservations-related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize the controller with the context to access the db and the class mapping to models
        /// </summary>
        public ReservationsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/reservations
        /// <summary>
        /// Get all the reservations in the database
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowReservationDto>>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Contact)
                .Include(r => r.User)
                .Include(r => r.Room)
                .ToListAsync();

            var showReservationDtos = _mapper.Map<List<ShowReservationDto>>(reservations);

            return showReservationDtos;
        }

        // GET api/reservations/{id}
        /// <summary>
        /// Get a reservation by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowReservationDto>> GetReservationById(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Contact)
                .Include(r => r.User)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var showReservationDto = _mapper.Map<ShowReservationDto>(reservation);

            return showReservationDto;
        }


        /// <summary>
        /// Creates a new reservation and a new contact
        /// </summary>
        // POST api/reservations
        [HttpPost]
        public async Task<ActionResult<ShowReservationDto>> CreateReservation(CreateReservationDto reservationDto)
        {
            var room = await _context.Rooms.FindAsync(reservationDto.RoomId);

            if (room == null)
            {
                return NotFound("The specified room does not exist");
            }

            var user = await _context.Users.FindAsync(reservationDto.UserId);

            if (user == null)
            {
                return NotFound("The specified user does not exist");
            }

            var contact = new Contact
            {
                ContactFirstName = reservationDto.ContactFirstName,
                ContactLastName = reservationDto.ContactLastName,
                ContactEmail = reservationDto.ContactEmail,
                ContactPhone = reservationDto.ContactPhone
            };

            var reservation = new Reservation
            {
                TotalCost = reservationDto.TotalCost,
                NumberOfGuests = reservationDto.NumberOfGuests,
                Contact = contact,
                User = user,
                Room = room,
                ReservationDate = reservationDto.ReservationDate,
                CheckInDate = reservationDto.CheckInDate,
                CheckOutDate = reservationDto.CheckOutDate
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var showReservationDto = _mapper.Map<ShowReservationDto>(reservation);

            return CreatedAtAction(nameof(GetReservationById), new { id = showReservationDto.Id }, showReservationDto);
        }

        // PUT api/reservations/{id}
        /// <summary>
        /// Updates an existing reservation
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ShowReservationDto>> UpdateReservation(int id, UpdateReservationDto reservationDto)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Contact)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(reservationDto.RoomId);

            if (room == null)
            {
                return NotFound("The specified room does not exist");
            }

            var contact = reservation.Contact;

            if (reservationDto.ContactFirstName != null)
            {
                contact.ContactFirstName = reservationDto.ContactFirstName;
            }

            if (reservationDto.ContactLastName != null)
            {
                contact.ContactLastName = reservationDto.ContactLastName;
            }

            if (reservationDto.ContactEmail != null)
            {
                contact.ContactEmail = reservationDto.ContactEmail;
            }

            reservation.CheckInDate = reservationDto.CheckInDate;
            reservation.CheckOutDate = reservationDto.CheckOutDate;
            reservation.Room = room;

            await _context.SaveChangesAsync();

            var showReservationDto = _mapper.Map<ShowReservationDto>(reservation);

            return showReservationDto;
        }



        /// <summary>
        /// Deletes an existing reservation
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET api/hotels/{hotelId}/reservations
        [HttpGet("{hotelId}/reservations")]
        public async Task<ActionResult<List<ShowReservationDto>>> GetReservationsByHotelId(int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);

            if (hotel == null)
            {
                return NotFound("The specified hotel does not exist");
            }

            var reservations = await _context.Reservations
                .Include(r => r.Contact)
                .Include(r => r.User)
                .Include(r => r.Room)
                .Where(r => r.Room.Hotel.Id == hotelId)
                .ToListAsync();

            var showReservationDtos = _mapper.Map<List<ShowReservationDto>>(reservations);

            return showReservationDtos;
        }


        [HttpGet("~/api/users/{userId}/reservations")]
        public async Task<ActionResult<IEnumerable<ShowReservationDto>>> GetReservationsByUserId(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations
                .Include(r => r.Contact)
                .Include(r => r.Room)
                .Where(r => r.User.Id == userId)
                .ToListAsync();

            var showReservationDtos = _mapper.Map<List<ShowReservationDto>>(reservations);

            return showReservationDtos;
        }
    }
}
