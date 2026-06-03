using Microsoft.AspNetCore.Mvc;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;
using Won.Shared.Common;

namespace Won.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _service;

        public ActivitiesController(IActivityService service)
        {
            _service = service;
        }

        [HttpGet("trip/{tripId}")]
        public async Task<IActionResult> GetByTrip(int tripId)
        {
            var activities =
                await _service.GetActivitiesByTripIdAsync(tripId);

            return Ok(new ApiResponse<List<ActivityDto>>
            {
                Success = true,
                StatusCode = 200,
                Message = "Activities retrieved successfully.",
                Data = activities,
                Errors = null
            });
        }

        [HttpGet("{activityId}")]
        public async Task<IActionResult> GetById(int activityId)
        {
            var activity =
                await _service.GetActivityByIdAsync(activityId);

            if (activity == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Activity not found."
                });
            }

            return Ok(new ApiResponse<ActivityDto>
            {
                Success = true,
                StatusCode = 200,
                Message = "Activity retrieved successfully.",
                Data = activity,
                Errors = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateActivityDto dto)
        {
            var created =
                await _service.CreateActivityAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { activityId = created.ActivityId },
                new ApiResponse<ActivityDto>
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Activity created successfully.",
                    Data = created
                });
        }

        [HttpPatch("{activityId}")]
        public async Task<IActionResult> Update(
            int activityId,
            UpdateActivityDto dto)
        {
            var updated =
                await _service.UpdateActivityAsync(activityId, dto);

            if (updated == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Activity not found."
                });
            }

            return Ok(new ApiResponse<ActivityDto>
            {
                Success = true,
                StatusCode = 200,
                Message = "Activity updated successfully.",
                Data = updated,
                Errors = null
            });
        }

        [HttpDelete("{activityId}")]
        public async Task<IActionResult> Delete(int activityId)
        {
            var deleted =
                await _service.DeleteActivityAsync(activityId);

            if (!deleted)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Activity not found."
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                StatusCode = 200,
                Message = "Activity deleted successfully.",
                Data = null,
                Errors = null
            });
        }
    }
}