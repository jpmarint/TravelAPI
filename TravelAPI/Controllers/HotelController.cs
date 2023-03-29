using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TravelAPI.Data;
using TravelAPI.DTO.Hotel;
using TravelAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAPI.Controllers
{
    /// <summary>
    /// Controller for handling hotel-related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize the controller with the context to access the db and the class mapping to models
        /// </summary>
        public HotelController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/hotel
        /// <summary>
        /// Get all hotels
        /// </summary>
        /// <returns>List of all hotels</returns>
        [HttpGet("", Name = "GetAllHotels")]
        [SwaggerOperation(Summary = "Get all hotels", Description = "Get a list of all hotels")]
        public async Task<ActionResult<IEnumerable<ShowHotelDto>>> GetAllHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            var hotelDtos = _mapper.Map<List<ShowHotelDto>>(hotels);
            return Ok(hotelDtos);
        }

        // GET: api/hotel/user/5
        /// <summary>
        /// Get all hotels by user id
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List of all hotels created by a specific user</returns>
        [HttpGet("user/{userId}")]
        [SwaggerOperation(Summary = "Get all hotels created by a specific user", Description = "Get a list of all hotels created by a specific user")]
        public async Task<ActionResult<IEnumerable<ShowHotelDto>>> GetAllHotelsByUserId(int userId)
        {
            var hotels = await _context.Hotels
                                .Where(h => h.User.Id == userId)
                                .ToListAsync();

            if (hotels == null || hotels.Count == 0)
            {
                return NotFound();
            }

            var hotelDtos = _mapper.Map<List<ShowHotelDto>>(hotels);
            return Ok(hotelDtos);
        }

        // GET: api/hotel/5
        /// <summary>
        /// Get a specific hotel by id
        /// </summary>
        /// <param name="id">Id of the hotel</param>
        /// <returns>Details of the specific hotel</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a specific hotel by id", Description = "Get the details of a specific hotel")]
        public async Task<ActionResult<ShowHotelDto>> GetHotelById(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDto = _mapper.Map<ShowHotelDto>(hotel);
            return Ok(hotelDto);
        }

        // GET: api/hotel/locations/{location}
        /// <summary>
        /// Get all hotels by location
        /// </summary>
        /// <param name="location">Location of the hotels</param>
        /// <returns>List of all hotels by location</returns>
        [HttpGet("locations/{location}")]
        [SwaggerOperation(Summary = "Get all hotels by location", Description = "Get a list of all hotels by location")]
        public async Task<ActionResult<IEnumerable<ShowHotelToTravelerDto>>> GetHotelsByLocation(string location)
        {
            var hotels = await _context.Hotels
                .Where(h => h.Location.ToLower().Contains(location.ToLower()))
                .ToListAsync();

            if (hotels == null || hotels.Count == 0)
            {
                return NotFound();
            }

            var showHotels = _mapper.Map<List<ShowHotelToTravelerDto>>(hotels);

            return Ok(showHotels);
        }

        // POST: api/hotel
        /// <summary>
        /// Create a new hotel
        /// </summary>
        /// <param name="createHotelDto">Data of the new hotel</param>
        /// <returns>Details of the created hotel</returns>
        [HttpPost]
        public async Task<ActionResult<ShowHotelDto>> CreateHotel(CreateHotelDto createHotelDto)
        {
            var user = await _context.Users.FindAsync(createHotelDto.UserId);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            var hotel = _mapper.Map<Hotel>(createHotelDto);
            hotel.User = user;
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            var hotelDto = _mapper.Map<ShowHotelDto>(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotelDto);
        }


        // PUT: api/hotel/5
        /// <summary>
        /// Update an existing hotel
        /// </summary>
        /// <param name="id">Id of the hotel to be updated</param>
        /// <param name="updateHotelDto">New data of the hotel</param>
        /// <returns>Result of the hotel update operation</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, UpdateHotelDto updateHotelDto)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(updateHotelDto, hotel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool HotelExist = _context.Hotels.Any(e => e.Id == id);
                if (!HotelExist)
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


        // DELETE: api/hotel/5
        /// <summary>
        /// Delete an existing hotel
        /// </summary>
        /// <param name="id">Id of the hotel to be deleted</param>
        /// <returns>Result of the hotel delete operation</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            var showHotelDto = _mapper.Map<ShowHotelDto>(hotel);

            return Ok(showHotelDto);
        }

    }
}
