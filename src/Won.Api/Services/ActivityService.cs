using Won.Api.Entities;
using Won.Api.Repositories.Interfaces;
using Won.Api.Services.Interfaces;
using Won.Shared.Dtos;

namespace Won.Api.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repository;

        public ActivityService(IActivityRepository repository)
        {
            _repository = repository;
        }
        private static ActivityDto MapToDto(Activity activity)
        {
            return new ActivityDto
            {
                ActivityId = activity.ActivityId,
                TripId = activity.TripId,
                Name = activity.Name,
                Cost = activity.Cost,
                WeatherDependency = activity.WeatherDependency,
                EnergyIntensity = activity.EnergyIntensity,
                MinimumGroupSize = activity.MinimumGroupSize,
                MaximumGroupSize = activity.MaximumGroupSize,
                ActivityDateTime = activity.ActivityDateTime
            };
        }

        public async Task<List<ActivityDto>> GetActivitiesByTripIdAsync(int tripId)
        {
            var activities =
                await _repository.GetActivitiesByTripIdAsync(tripId);

            return activities
                .Select(MapToDto)
                .ToList();
        }

        public async Task<ActivityDto?> GetActivityByIdAsync(int activityId)
        {
            var activity =
                await _repository.GetActivityByIdAsync(activityId);

            if (activity == null)
            {
                return null;
            }

            return MapToDto(activity);
        }

        public async Task<ActivityDto> CreateActivityAsync(
            CreateActivityDto activityData)
        {
            var activity = new Activity
            {
                TripId = activityData.TripId,
                Name = activityData.Name,
                Cost = activityData.Cost,
                WeatherDependency = activityData.WeatherDependency,
                EnergyIntensity = activityData.EnergyIntensity,
                MinimumGroupSize = activityData.MinimumGroupSize,
                MaximumGroupSize = activityData.MaximumGroupSize,
                ActivityDateTime = activityData.ActivityDateTime
            };

            var created =
                await _repository.CreateActivityAsync(activity);

            return MapToDto(created);
        }

        public async Task<ActivityDto?> UpdateActivityAsync(
            int activityId,
            UpdateActivityDto updatedActivityData)
        {
            var activity =
                await _repository.GetActivityByIdAsync(activityId);

            if (activity == null)
            {
                return null;
            }
            activity.TripId = updatedActivityData.TripId;
            activity.Name = updatedActivityData.Name;
            activity.Cost = updatedActivityData.Cost;
            activity.WeatherDependency = updatedActivityData.WeatherDependency;
            activity.EnergyIntensity = updatedActivityData.EnergyIntensity;
            activity.MinimumGroupSize = updatedActivityData.MinimumGroupSize;
            activity.MaximumGroupSize = updatedActivityData.MaximumGroupSize;
            activity.ActivityDateTime = updatedActivityData.ActivityDateTime;

            var updated =
                await _repository.UpdateActivityAsync(activity);

            if (updated == null)
            {
                return null;
            }

            return MapToDto(updated);
        }

        public async Task<bool> DeleteActivityAsync(int activityId)
        {
            return await _repository
                .DeleteActivityAsync(activityId);
        }
    }
}
