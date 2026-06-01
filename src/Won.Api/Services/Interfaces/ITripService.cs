using Won.Shared.Dtos;

namespace Won.Api.Services.Interfaces
{
    public interface ITripService
    {
        Task<List<TripDto>> GetTripsAsync();
        Task<TripDto?> GetTripByIdAsync(int id);
        Task<TripDto> CreateTripAsync(CreateTripDto dto);
        Task<TripDto?> UpdateTripAsync(int id, UpdateTripDto dto);
        Task<bool> DeleteTripAsync(int id);
    }
}
