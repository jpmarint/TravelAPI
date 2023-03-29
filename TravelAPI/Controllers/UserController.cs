using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TravelAPI.Data;
using TravelAPI.DTO.User;
using TravelAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<UserController>
        /// <summary>
        /// Retrieves all users
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all users")]
        public async Task<ActionResult<IEnumerable<ShowUserDto>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ShowUserDto>>(users));
        }

        // GET api/<UserController>/5
        /// <summary>
        /// Retrieves a specific user by id
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves a specific user by id")]
        public async Task<ActionResult<ShowUserDto>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ShowUserDto>(user));
        }

        // GET api/<UserController>/GetByEmail
        /// <summary>
        /// Retrieves a specific user by email
        /// </summary>
        [HttpGet("GetByEmail")]
        [SwaggerOperation(Summary = "Retrieves a specific user by email")]
        public async Task<ActionResult<ShowUserDto>> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ShowUserDto>(user));
        }

        // POST api/<UserController>
        /// <summary>
        /// Registers a new user
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Registers a new user")]
        public async Task<ActionResult<ShowUserDto>> RegisterUser([FromBody] CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var showUserDto = _mapper.Map<ShowUserDto>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, showUserDto);
        }


        // PUT api/<UserController>/5
        /// <summary>
        /// Updates an existing user
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing user")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Password = updateUserDto.Password;
            user.Gender = updateUserDto.Gender;
            user.Role = updateUserDto.Role;
            user.DocumentType = updateUserDto.DocumentType;
            user.DocumentNumber = updateUserDto.DocumentNumber;
            user.Phone = updateUserDto.Phone;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var showUserDto = _mapper.Map<ShowUserDto>(user);
            return Ok(showUserDto);
        }

        // DELETE api/<UserController>/5
        /// <summary>
        /// Deletes a user
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a user")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
