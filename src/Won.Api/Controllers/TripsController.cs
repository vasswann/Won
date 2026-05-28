using Microsoft.AspNetCore.Mvc;
using Won.Api.Services.Interfaces;

namespace Won.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _tripService.GetTripsAsync();

            return Ok(trips);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(int id)
        {
            var trip = await _tripService.GetTripByIdAsync(id);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }
    }
}
