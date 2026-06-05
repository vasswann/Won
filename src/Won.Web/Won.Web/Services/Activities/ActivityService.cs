using Won.Shared.Common;
using Won.Shared.Dtos;

namespace Won.Web.Services.Activities
{
    public class ActivityService
    {
        private readonly HttpClient _httpClient;

        public ActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ActivityDto>> GetActivitiesByTripIdAsync(int tripId)
        {
            var response =
                await _httpClient.GetFromJsonAsync<ApiResponse<List<ActivityDto>>>($"api/activities/trip/{tripId}");

            if (response?.Data == null)
            {
                return [];
            }

            return response.Data;
        }

        public async Task<ActivityDto?> GetActivityByIdAsync(int activityId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<ActivityDto>>($"api/activities/{activityId}");

            return response?.Data;
        }

        public async Task<ActivityDto?> CreateActivityAsync(CreateActivityDto activityData)
        {
            var response = await _httpClient.PostAsJsonAsync("api/activities", activityData);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ActivityDto>>();

            return apiResponse?.Data;
        }

        public async Task<ActivityDto?> UpdateActivityAsync(int activityId, UpdateActivityDto updatedActivityData)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/activities/{activityId}", updatedActivityData);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ActivityDto>>();

            return apiResponse?.Data;
        }

        public async Task<bool> DeleteActivityAsync(int activityId)
        {
            var response = await _httpClient.DeleteAsync($"api/activities/{activityId}");

            return response.IsSuccessStatusCode;
        }
    }
}
