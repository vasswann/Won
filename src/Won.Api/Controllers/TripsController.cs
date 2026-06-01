using Microsoft.AspNetCore.Mvc;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;
using Won.Shared.Common;

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

            return Ok(new ApiResponse<List<TripDto>>
            {
                Success = true,
                StatusCode = 200,
                Message = "Trips retrieved successfully.",
                Data = trips,
                Errors = null
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(int id)
        {
            var trip = await _tripService.GetTripByIdAsync(id);

            if (trip == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Trip not found."
                });
            }

            return Ok(new ApiResponse<TripDto>
            {
                Success = true,
                StatusCode = 200,
                Message = "Trip retrieved successfully.",
                Data = trip,
                Errors = null
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripDto tripData)
        {
            var trip = await _tripService.CreateTripAsync(tripData);

            return CreatedAtAction(
                nameof(GetTripById),
                new { id = trip.TripId },
                new ApiResponse<TripDto>
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Trip created successfully.",
                    Data = trip
                });
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTrip(int id, UpdateTripDto updatedTripData)
        {
            var updatedTrip = await _tripService.UpdateTripAsync(id, updatedTripData);

            if (updatedTrip == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Trip not found."
                });
            }

            return Ok(new ApiResponse<TripDto>
            {
                Success = true,
                StatusCode = 200,
                Message = "Trip updated successfully.",
                Data = updatedTrip,
                Errors = null
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var deleted = await _tripService.DeleteTripAsync(id);

            if (!deleted)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Trip not found."
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                StatusCode = 200,
                Message = "Trip deleted successfully.",
                Data = null,
                Errors = null
            });
        }
    }
}
