using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAPI.Data;

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


    }
}
