using Won.Shared.Dtos;

namespace Won.Api.Services.Interfaces
{
    public interface IActivityRecommendationService
    {
        Task<List<RecommendedActivityDto>>
            GetRecommendationsAsync(
                int tripId,
                UserActivityPreferenceDto preferences);
    }
}
