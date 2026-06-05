using Won.Shared.Common;
using Won.Shared.Dtos;

namespace Won.Web.Services.Trips;

public class TripsService
{
    private readonly HttpClient _httpClient;

    public TripsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TripListItemDto>> GetTripsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<TripDto>>>("api/trips");

        if (response?.Data == null)
        {
            return [];
        }

        return response.Data.Select(trip => new TripListItemDto
        {
            TripId = trip.TripId,
            Name = trip.Name,
            Location = trip.Location,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate
        }).ToList();
    }

    public async Task<TripDto?> GetTripByIdAsync(int tripId)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<TripDto>>($"api/trips/{tripId}");

        return response?.Data;
    }

    public async Task<ApiResponse<TripDto>> CreateTripAsync(CreateTripDto tripData)
    {
        var response = await _httpClient.PostAsJsonAsync("api/trips", tripData);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<TripDto>>();

        return result ?? new ApiResponse<TripDto>
               {
                   Success = false,
                   StatusCode = (int)response.StatusCode,
                   Message = "Unable to process create trip response."
               };
    }

    public async Task<ApiResponse<TripDto>> UpdateTripAsync(int tripId, UpdateTripDto tripData)
    {
        var response = await _httpClient.PatchAsJsonAsync($"api/trips/{tripId}", tripData);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<TripDto>>();

        return result ?? new ApiResponse<TripDto>
               {
                   Success = false,
                   StatusCode = (int)response.StatusCode,
                   Message = "Unable to process update trip response."
               };
    }

    public async Task<ApiResponse<object>> DeleteTripAsync(int tripId)
    {
        var response = await _httpClient.DeleteAsync($"api/trips/{tripId}");

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        return result ?? new ApiResponse<object>
               {
                   Success = false,
                   StatusCode = (int)response.StatusCode,
                   Message = "Unable to process delete trip response."
               };
    }
}