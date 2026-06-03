using Won.Shared.Dtos;

namespace Won.Api.Services.Interfaces
{
    public interface IActivityService
    {
        Task<List<ActivityDto>> GetActivitiesByTripIdAsync(int tripId);
        Task<ActivityDto?> GetActivityByIdAsync(int activityId);
        Task<ActivityDto> CreateActivityAsync(CreateActivityDto activityData);
        Task<ActivityDto?> UpdateActivityAsync(int activityId, UpdateActivityDto updatedActivityData);
        Task<bool> DeleteActivityAsync(int activityId);
    }
}