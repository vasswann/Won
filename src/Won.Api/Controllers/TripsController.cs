using Microsoft.AspNetCore.Mvc;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;

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
        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripDto tripData)
        {
            var trip = await _tripService.CreateTripAsync(tripData);

            return Ok(trip);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTrip(int id, UpdateTripDto updatedTripData)
        {
            var updatedTrip = await _tripService.UpdateTripAsync(id, updatedTripData);

            if (updatedTrip == null)
            {
                return NotFound();
            }

            return Ok(updatedTrip);
        }
    }
}
