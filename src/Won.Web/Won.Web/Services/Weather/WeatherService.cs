using System.Text.Json.Serialization;

using Won.Shared.Dtos;

namespace Won.Web.Services.Weather;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<WeatherForecastDto>> GetWeatherForecastAsync(
        string location,
        DateOnly startDate,
        DateOnly endDate)
    {
        var geoUrl =
            $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(location)}&count=1&language=en&format=json";

        var geoResponse = await _httpClient.GetFromJsonAsync<GeocodingResponse>(geoUrl);

        var place = geoResponse?.Results?.FirstOrDefault();

        if (place is null)
        {
            return [];
        }

        var forecastUrl =
            $"https://api.open-meteo.com/v1/forecast?latitude={place.Latitude}&longitude={place.Longitude}&daily=weather_code,temperature_2m_max,temperature_2m_min&timezone=auto&start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";

        var forecastHttpResponse = await _httpClient.GetAsync(forecastUrl);

        if (!forecastHttpResponse.IsSuccessStatusCode)
        {
            return [];
        }

        var forecastResponse =
            await forecastHttpResponse.Content.ReadFromJsonAsync<ForecastResponse>();

        if (forecastResponse?.Daily is null)
        {
            return [];
        }

        var result = new List<WeatherForecastDto>();

        var count = new[]
        {
            forecastResponse.Daily.Time.Count,
            forecastResponse.Daily.WeatherCode.Count,
            forecastResponse.Daily.TemperatureMax.Count,
            forecastResponse.Daily.TemperatureMin.Count
        }.Min();

        for (var i = 0; i < count; i++)
        {
            var code = forecastResponse.Daily.WeatherCode[i];

            result.Add(new WeatherForecastDto
            {
                Date = DateOnly.Parse(forecastResponse.Daily.Time[i]),
                MaxTemperature = forecastResponse.Daily.TemperatureMax[i],
                MinTemperature = forecastResponse.Daily.TemperatureMin[i],
                WeatherCode = code,
                Description = GetWeatherDescription(code),
                Icon = GetWeatherIcon(code)
            });
        }

        return result;
    }

    private static string GetWeatherDescription(int code)
    {
        return code switch
        {
            0 => "Clear",
            1 or 2 or 3 => "Partly cloudy",
            45 or 48 => "Fog",
            >= 51 and <= 57 => "Drizzle",
            >= 61 and <= 67 => "Rain",
            >= 71 and <= 77 => "Snow",
            >= 80 and <= 82 => "Rain showers",
            >= 85 and <= 86 => "Snow showers",
            >= 95 and <= 99 => "Thunderstorm",
            _ => "Unknown"
        };
    }

    private static string GetWeatherIcon(int code)
    {
        return code switch
        {
            0 => "☀️",
            1 or 2 or 3 => "⛅",
            45 or 48 => "🌫️",
            >= 51 and <= 57 => "🌦️",
            >= 61 and <= 67 => "🌧️",
            >= 71 and <= 77 => "❄️",
            >= 80 and <= 82 => "🌧️",
            >= 85 and <= 86 => "❄️",
            >= 95 and <= 99 => "⛈️",
            _ => "🌡️"
        };
    }

    private sealed class GeocodingResponse
    {
        public List<GeocodingResult>? Results { get; set; }
    }

    private sealed class GeocodingResult
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    private sealed class ForecastResponse
    {
        public ForecastDaily? Daily { get; set; }
    }

    private sealed class ForecastDaily
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; } = [];

        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; } = [];

        [JsonPropertyName("temperature_2m_max")]
        public List<double> TemperatureMax { get; set; } = [];

        [JsonPropertyName("temperature_2m_min")]
        public List<double> TemperatureMin { get; set; } = [];
    }
}