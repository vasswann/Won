using Microsoft.AspNetCore.Mvc;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;
using Won.Shared.Common;

namespace Won.Api.Controllers
{
    [ApiController]
    [Route("api/trips/{tripId}/recommendations")]
    public class RecommendationsController : ControllerBase
    {
        private readonly IActivityRecommendationService
            _recommendationService;

        public RecommendationsController(
            IActivityRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpPost]
        public async Task<IActionResult> GetRecommendations(
            int tripId,
            UserActivityPreferenceDto preferences)
        {
            var recommendations =
                await _recommendationService
                    .GetRecommendationsAsync(
                        tripId,
                        preferences);

            return Ok(
                new ApiResponse<List<RecommendedActivityDto>>
                {
                    Success = true,
                    StatusCode = 200,
                    Message =
                        "Recommendations generated successfully.",
                    Data = recommendations
                });
        }
    }
}
