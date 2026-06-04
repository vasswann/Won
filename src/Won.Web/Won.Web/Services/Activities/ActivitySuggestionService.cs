using Won.Shared.Common;
using Won.Shared.Dtos;

namespace Won.Web.Services.Activities
{
    public class ActivitySuggestionService
    {
        private readonly HttpClient _httpClient;

        public ActivitySuggestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<ActivitySuggestionDto>>> GetActivitySuggestionsAsync(ActivitySuggestionRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/AiActivitySuggestions/suggestions", request);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<List<ActivitySuggestionDto>>
                {
                    Success = false,
                    StatusCode = (int)response.StatusCode,
                    Message = "Activity suggestions could not be generated",
                    Data = []
                };
            }

            var result =  await response.Content.ReadFromJsonAsync<ApiResponse<List<ActivitySuggestionDto>>>();

            return result ??
                new ApiResponse<List<ActivitySuggestionDto>>()
                {
                    Success = false,
                    StatusCode = 500,
                    Message = "Unable to read activity suggestions response.",
                    Data = []
                };
        }
    }
}
