using Won.Shared.Common;
using Won.Shared.Dtos;

namespace Won.Api.Services.Interfaces
{
    public interface IAiActivitySuggestionService
    {
        Task<ApiResponse<List<ActivitySuggestionDto>>> GenerateActivitySuggestionsAsync(
            ActivitySuggestionRequestDto request);
    }
}
