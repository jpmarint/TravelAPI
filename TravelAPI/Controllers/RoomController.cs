using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAPI.Data;
using TravelAPI.DTO.Room;
using TravelAPI.Models;

namespace TravelAPI.Controllers
{
    /// <summary>
    /// Controller for handling room-related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize the controller with the context to access the db and the class mapping to models
        /// </summary>
        public RoomController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/room
        /// <summary>
        /// Gets all rooms.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowRoomDto>>> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            var showRooms = _mapper.Map<List<ShowRoomDto>>(rooms);
            return Ok(showRooms);
        }

        /// <summary>
        /// Gets the rooms by hotel identifier.
        /// </summary>
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<ShowRoomDto>>> GetRoomsByHotelId(int hotelId)
        {
            var rooms = await _context.Rooms.Where(r => r.Hotel.Id == hotelId).ToListAsync();
            if (rooms == null || rooms.Count == 0)
            {
                return NotFound();
            }

            var showRooms = _mapper.Map<List<ShowRoomDto>>(rooms);
            return Ok(showRooms);
        }

        // GET: api/room/5
        /// <summary>
        /// Gets the room by identifier.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowRoomDto>> GetRoomById(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            var showRoom = _mapper.Map<ShowRoomDto>(room);
            return Ok(showRoom);
        }


        // POST: api/room
        /// <summary>
        /// Creates a new room.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ShowRoomDto>> CreateRoom(UpsertRoomDto upsertRoomDto)
        {
            var hotel = await _context.Hotels.FindAsync(upsertRoomDto.HotelId);
            if (hotel == null)
            {
                return BadRequest("Hotel not found");
            }

            var room = _mapper.Map<Room>(upsertRoomDto);
            room.Hotel = hotel;
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var showRoom = _mapper.Map<ShowRoomDto>(room);
            return CreatedAtAction(nameof(GetRoomById), new { id = showRoom.Id }, showRoom);
        }

        // PUT: api/room/5
        /// <summary>
        /// Updates an existing room.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, UpsertRoomDto upsertRoomDto)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _mapper.Map(upsertRoomDto, room);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool RoomExists = _context.Rooms.Any(room => room.Id == id);
                if (!RoomExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing room.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
