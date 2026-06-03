using Won.Api.Entities;

namespace Won.Api.Repositories.Interfaces
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetActivitiesByTripIdAsync(int tripId);
        Task<Activity?> GetActivityByIdAsync(int activityId);
        Task<Activity> CreateActivityAsync(Activity activity);
        Task<Activity?> UpdateActivityAsync(Activity activity);
        Task<bool> DeleteActivityAsync(int activityId);
    }
}
