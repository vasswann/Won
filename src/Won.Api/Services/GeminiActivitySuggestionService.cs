using System.Text.Json;

using Won.Api.Exceptions;
using Won.Api.Services.GeminiModels;
using Won.Api.Services.Interfaces;
using Won.Shared.Common;
using Won.Shared.Dtos;

namespace Won.Api.Services;

public class GeminiActivitySuggestionService : IAiActivitySuggestionService
{
    private readonly HttpClient _httpClient;
    private const string GeminiModel = "gemini-2.5-flash";

    public GeminiActivitySuggestionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<List<ActivitySuggestionDto>>>
        GenerateActivitySuggestionsAsync(ActivitySuggestionRequestDto request)
    {
        var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new BadRequestException("Gemini API key is missing.");
        }

        var prompt = $@"
        You are a travel activity suggestion assistant.

        Suggest exactly 3 activities for this trip.

        Trip location:
        {request.Location}

        Trip dates:
        {request.StartDate:yyyy-MM-dd} to {request.EndDate:yyyy-MM-dd}

        Trip budget:
        {request.Budget}

        Group size:
        {request.GroupSize}

        Return only valid JSON in this format:
        [
          {{
            ""title"": ""Activity title"",
            ""description"": ""Short activity description"",
            ""difficulty"": ""Easy, Medium, or Hard"",
            ""duration"": ""Estimated duration"",
            ""estimatedCost"": ""Estimated cost""
          }}
        ]

        Do not include markdown.
        Do not include explanations outside the JSON.
        ";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
        };

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{GeminiModel}:generateContent?key={apiKey}";

        var response = await _httpClient.PostAsJsonAsync(url, requestBody);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new BadRequestException(responseContent);
        }

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent, jsonOptions);

        var text = geminiResponse?.Candidates?.FirstOrDefault() ?.Content?.Parts?.FirstOrDefault() ?.Text;

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new BadRequestException("Gemini returned an empty response.");
        }

        var cleanedJson = text
            .Replace("```json", string.Empty)
            .Replace("```", string.Empty)
            .Trim();

        var suggestions = JsonSerializer.Deserialize<List<ActivitySuggestionDto>>(cleanedJson, jsonOptions);

        if (suggestions == null || suggestions.Count == 0)
        {
            throw new BadRequestException("Gemini response could not be parsed.");
        }

        return new ApiResponse<List<ActivitySuggestionDto>>
        {
            Success = true,
            StatusCode = 200,
            Message = "Activity suggestions generated successfully.",
            Data = suggestions
        };
    }
}