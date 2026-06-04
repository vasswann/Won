using Won.Shared.Common;
using Won.Shared.Dtos;

namespace Won.Web.Services.Trips
{
    public class TripsService
    {
        private readonly HttpClient _httpClient;

        public TripsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<TripListItemDto>> GetTripsAsync()
        {
            var response =
                await _httpClient.GetFromJsonAsync<ApiResponse<List<TripDto>>>("api/trips");

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
            var response =
                await _httpClient.GetFromJsonAsync<ApiResponse<TripDto>>
                ($"api/trips/{tripId}");

            return response?.Data;
        }

        public async Task<TripDto?> CreateTripAsync(CreateTripDto tripData)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/Trips",
                    tripData);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse =
                await response.Content.ReadFromJsonAsync<ApiResponse<TripDto>>();

            return apiResponse?.Data;
        }

        public async Task<TripDto?> UpdateTripAsync(
            int tripId, 
            UpdateTripDto tripData)
        {
            var response =
                await _httpClient.PatchAsJsonAsync(
                    $"api/Trips/{tripId}",
                    tripData);

            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse =
                await response.Content.ReadFromJsonAsync<ApiResponse<TripDto>>();

            return apiResponse?.Data;
        }

        public async Task<bool> DeleteTripAsync(int tripId)
        {
            var response =
                await _httpClient.DeleteAsync(
                    $"api/Trips/{tripId}");

                return response.IsSuccessStatusCode;

       
        }
    }
}