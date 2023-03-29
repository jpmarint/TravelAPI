using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


    }
}
