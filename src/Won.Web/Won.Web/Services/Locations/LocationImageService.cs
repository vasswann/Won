using System.Text.Json.Serialization;

using Won.Shared.Dtos;

namespace Won.Web.Services.Locations;

public class LocationImageService
{
    private readonly HttpClient _httpClient;

    public LocationImageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LocationImageDto?> GetLocationImageAsync(string location)
    {
        var url =
            $"https://en.wikipedia.org/api/rest_v1/page/summary/{Uri.EscapeDataString(location)}";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.UserAgent.ParseAdd("WonTravelApp/1.0");

        var httpResponse = await _httpClient.SendAsync(request);

        if (!httpResponse.IsSuccessStatusCode)
        {
            return null;
        }

        var response =
            await httpResponse.Content.ReadFromJsonAsync<WikipediaSummaryResponse>();

        if (response is null)
        {
            return null;
        }

        var imageUrl =
            response.OriginalImage?.Source ??
            response.Thumbnail?.Source;

        return new LocationImageDto
        {
            ImageUrl = imageUrl,
            Title = response.Title,
            Extract = response.Extract
        };
    }

    private sealed class WikipediaSummaryResponse
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("extract")]
        public string? Extract { get; set; }

        [JsonPropertyName("thumbnail")]
        public WikipediaImage? Thumbnail { get; set; }

        [JsonPropertyName("originalimage")]
        public WikipediaImage? OriginalImage { get; set; }
    }

    private sealed class WikipediaImage
    {
        [JsonPropertyName("source")]
        public string? Source { get; set; }
    }
}