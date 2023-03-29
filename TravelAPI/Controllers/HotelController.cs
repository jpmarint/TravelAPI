using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TravelAPI.Data;
using TravelAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HotelController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/hotel
        [HttpGet("", Name = "GetAllHotels")]
        [SwaggerOperation(Summary = "Get all hotels", Description = "Get all hotels")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        // GET: api/hotel/user/5
        [HttpGet("api/hotel/user/{userId}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotelsByUserId(int userId)
        {
            var hotels = await _context.Hotels
                                .Where(h => h.User.Id == userId)
                                .ToListAsync();

            if (hotels == null || hotels.Count == 0)
            {
                return NotFound();
            }

            return Ok(hotels);
        }

        // GET: api/hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotelById(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // POST: api/hotel
        [HttpPost]
        public async Task<ActionResult<Hotel>> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
        }

        // PUT: api/hotel/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Hotel>> UpdateHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

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

            return Ok(hotel);
        }

        // DELETE: api/hotel/5
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

            return NoContent();
        }

    }
}
