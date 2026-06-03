using Won.Api.Repositories.Interfaces;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;

namespace Won.Api.Services
{
    public class ActivityRecommendationService
        : IActivityRecommendationService
    {
        private readonly ITripRepository _tripRepository;

        public ActivityRecommendationService(
            ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<List<RecommendedActivityDto>>
            GetRecommendationsAsync(
                int tripId,
                UserActivityPreferenceDto preferences)
        {
            var trip =
                await _tripRepository.GetTripByIdAsync(tripId);

            if (trip == null)
            {
                return new List<RecommendedActivityDto>();
            }

            // TODO:
            // 1. Get weather
            // 2. Build AI prompt
            // 3. Call Gemini/OpenAI
            // 4. Return parsed recommendations

            return new List<RecommendedActivityDto>();
        }
    }
}
